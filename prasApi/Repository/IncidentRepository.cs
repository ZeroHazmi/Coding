using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using prasApi.Data;
using prasApi.Interfaces;
using prasApi.Models;

namespace prasApi.Repository
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly ApplicationDbContext _context;

        public IncidentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<DemographicData>> GetDemographicDataAsync(string? gender, int? minAge, int? maxAge, string? priority, int reportTypeId)
        {
            var query = _context.Reports
                .Include(r => r.ReportType)
                .Include(r => r.AppUser)
                .AsQueryable();

            // Apply gender filter
            if (!string.IsNullOrEmpty(gender) && Enum.TryParse<Gender>(gender, true, out var genderEnum))
            {
                query = query.Where(r => r.AppUser.Gender == genderEnum);
            }

            // Calculate age based on birthdate and apply minAge and maxAge filters
            if (minAge.HasValue || maxAge.HasValue)
            {
                var today = DateTime.UtcNow;
                query = query.Where(r =>
                    (!minAge.HasValue || today.Year - r.AppUser.Birthday.Year >= minAge) &&
                    (!maxAge.HasValue || today.Year - r.AppUser.Birthday.Year <= maxAge));
            }

            // Apply report type filter
            query = query.Where(r => r.ReportType.Id == reportTypeId);

            // Group the data
            var demographicData = await query
                .GroupBy(r => new
                {
                    Gender = r.AppUser.Gender,
                    Age = DateTime.UtcNow.Year - r.AppUser.Birthday.Year,
                    ReportTypeId = r.ReportType.Id
                })
                .Select(g => new DemographicData
                {
                    Gender = g.Key.Gender.ToString(),
                    Age = g.Key.Age,
                    ReportTypeId = g.Key.ReportTypeId,
                    IncidentCount = g.Count()
                })
                .ToListAsync();

            return demographicData;
        }

        public async Task<List<HeatMapData>> GetHeatmapDataAsync(string? priority, int? reportTypeId)
        {
            var query = _context.Reports
                .Include(r => r.ReportDetail)
                .Include(r => r.ReportType)
                .AsQueryable();

            // Apply priority filter (if priority is provided)
            if (!string.IsNullOrEmpty(priority) && Enum.TryParse<Priority>(priority, true, out var priorityEnum))
            {
                query = query.Where(r => r.Priority == priorityEnum);
            }
            // If priority is null, no filtering on priority is applied

            // Apply report type filter (if reportTypeId is provided)
            if (reportTypeId.HasValue)
            {
                query = query.Where(r => r.ReportType.Id == reportTypeId.Value);
            }

            // Group the data
            var heatmapData = await query
                .GroupBy(r => new { r.ReportDetail.Latitude, r.ReportDetail.Longitude, r.ReportType.Id })
                .Select(g => new HeatMapData
                {
                    Latitude = g.Key.Latitude,
                    Longitude = g.Key.Longitude,
                    ReportTypeId = g.Key.Id,
                })
                .ToListAsync();

            return heatmapData;
        }

        public async Task<List<IncidentRateData>> GetIncidentRateDataAsync(
            string timeRange = "daily",
            string state = "",
            int? reportTypeId = null,
            string dateFilterType = "submissionDate",
            DateTime? startDate = null,
            DateTime? endDate = null)
        {
            var query = _context.Reports
                .Include(r => r.ReportDetail)
                .AsQueryable();

            // Filter by state
            if (!string.IsNullOrEmpty(state) && state != "All States")
            {
                query = query.Where(r => r.ReportDetail.State == state);
            }

            // Filter by ReportTypeId
            if (reportTypeId.HasValue)
            {
                query = query.Where(r => r.ReportDetail.ReportTypeId == reportTypeId.Value);
            }

            // Date filter logic
            if (startDate.HasValue && endDate.HasValue)
            {
                var start = startDate.Value.ToUniversalTime();
                var end = endDate.Value.ToUniversalTime();

                if (dateFilterType == "date")
                {
                    query = query.Where(r => r.ReportDetail.Date >= start && r.ReportDetail.Date <= end);
                }
                else if (dateFilterType == "submissionDate")
                {
                    query = query.Where(r => r.CreatedAt >= start && r.CreatedAt <= end);
                }
            }
            else
            {
                if (timeRange == "daily")
                {
                    var now = DateTime.UtcNow.Date;
                    query = query.Where(r => r.ReportDetail.Date >= now);
                }
                else if (timeRange == "weekly")
                {
                    var now = DateTime.UtcNow.Date;
                    var startOfWeek = now.AddDays(-7);
                    query = query.Where(r => r.ReportDetail.Date >= startOfWeek && r.ReportDetail.Date <= now);
                }
                else if (timeRange == "monthly")
                {
                    var now = DateTime.UtcNow.Date;
                    var startOfMonth = now.AddMonths(-1);
                    query = query.Where(r => r.ReportDetail.Date >= startOfMonth && r.ReportDetail.Date <= now);
                }
            }

            // Group and project the data
            var incidentRateData = await query
                .GroupBy(r => new
                {
                    Date = r.ReportDetail.Date.Date,
                    ReportTypeId = r.ReportType.Id
                })
                .Select(g => new IncidentRateData
                {
                    ReportTypeId = g.Key.ReportTypeId,
                    ReportTypeName = g.First().ReportType.Name,
                    IncidentCount = g.Count()
                })
                .ToListAsync();

            return incidentRateData;
        }


    }
}