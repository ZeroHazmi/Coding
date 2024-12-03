using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using prasApi.Data;
using prasApi.Dtos.Incident;
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

        public async Task<List<DemographicData>> GetDemographicDataAsync(
            Gender? gender,
            int? minAge,
            int? maxAge,
            Priority? priority,
            string? ageRange,
            int reportTypeId)
        {
            var today = DateTime.UtcNow;

            var query = _context.Reports
                .Include(r => r.ReportType)
                .Include(r => r.AppUser)
                .Where(r => r.AppUser != null && r.ReportType.Id == reportTypeId);

            // Gender filter
            if (gender.HasValue)
            {
                query = query.Where(r => r.AppUser.Gender == gender.Value);
            }

            // Age filtering with safe conversion
            if (ageRange != "All")
            {
                if (minAge.HasValue)
                {
                    query = query.Where(r => (today.Year - r.AppUser.Birthday.Year) >= minAge.Value);
                }

                if (maxAge.HasValue)
                {
                    query = query.Where(r => (today.Year - r.AppUser.Birthday.Year) <= maxAge.Value);
                }
            }

            // Priority filter
            if (priority.HasValue)
            {
                query = query.Where(r => r.Priority == priority.Value);
            }

            // Demographic data aggregation
            var demographicData = await query
                .GroupBy(r => new
                {
                    Gender = r.AppUser.Gender,
                    Age = today.Year - r.AppUser.Birthday.Year,
                    IncidentType = r.ReportType.Name,
                    Priority = r.Priority
                })
                .Select(g => new DemographicData
                {
                    Gender = g.Key.Gender.ToString(),
                    Age = g.Key.Age,
                    ReportTypeId = reportTypeId,
                    ReportTypeName = g.Key.IncidentType,
                    Priority = g.Key.Priority,
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
            string? timeRange = "daily",
            string? state = "",
            int? reportTypeId = null,
            string? dateFilterType = "submissionDate",
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

            // Group by ReportTypeId and count incidents
            var incidentRateData = await query
                .GroupBy(r => new { r.ReportType.Id, r.ReportType.Name })
                .Select(g => new IncidentRateData
                {
                    ReportTypeId = g.Key.Id,
                    ReportTypeName = g.Key.Name,
                    IncidentCount = g.Count()
                })
                .ToListAsync();

            return incidentRateData;
        }


    }
}