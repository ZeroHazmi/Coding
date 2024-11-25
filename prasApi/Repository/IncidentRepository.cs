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

        public async Task<List<DemographicData>> GetDemographicDataAsync(string gender, int? minAge, int? maxAge, int reportTypeId)
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

        public async Task<List<HeatMapData>> GetHeatmapDataAsync(string priority, int reportTypeId)
        {
            var query = _context.Reports
                .Include(r => r.ReportDetail)
                .Include(r => r.ReportType)
                .AsQueryable();

            // Apply priority filter
            if (!string.IsNullOrEmpty(priority) && Enum.TryParse<Priority>(priority, true, out var priorityEnum))
            {
                query = query.Where(r => r.Priority == priorityEnum);
            }

            // Apply report type filter
            query = query.Where(r => r.ReportType.Id == reportTypeId);

            // Group the data
            var heatmapData = await query
                .GroupBy(r => new { r.ReportDetail.Latitude, r.ReportDetail.Longitude, r.ReportType.Id })
                .Select(g => new HeatMapData
                {
                    Latitude = g.Key.Latitude,
                    Longitude = g.Key.Longitude,
                    ReportTypeId = g.Key.Id,
                    IncidentCount = g.Count()
                })
                .ToListAsync();

            return heatmapData;
        }

        public async Task<List<IncidentRateData>> GetIncidentRateDataAsync(string state, int reportTypeId, string priority, DateTime? startDate, DateTime? endDate)
        {
                        var query = _context.Reports
                .Include(r => r.ReportDetail)
                .Include(r => r.ReportType)
                .AsQueryable();

            // Apply filters
            if (!string.IsNullOrEmpty(state))
            {
                query = query.Where(r => r.ReportDetail.State == state);
            }

            query = query.Where(r => r.ReportType.Id == reportTypeId);

            if (!string.IsNullOrEmpty(priority) && Enum.TryParse<Priority>(priority, true, out var priorityEnum))
            {
                query = query.Where(r => r.Priority == priorityEnum);
            }

            if (startDate.HasValue)
            {
                query = query.Where(r => r.ReportDetail.Date >= startDate.Value);
            }

            if (endDate.HasValue)
            {
                query = query.Where(r => r.ReportDetail.Date <= endDate.Value);
            }

            // Group by date and report type
            var incidentRateData = await query
                .GroupBy(r => new { r.ReportDetail.Date.Date, r.ReportType.Id })
                .Select(g => new IncidentRateData
                {
                    Date = g.Key.Date,
                    ReportTypeId = g.Key.Id,
                    IncidentCount = g.Count()
                })
                .ToListAsync();

            return incidentRateData;
        }
    }
}