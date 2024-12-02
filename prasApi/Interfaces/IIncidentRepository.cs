using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prasApi.Models;

namespace prasApi.Interfaces
{
    public interface IIncidentRepository
    {
        Task<List<DemographicData>> GetDemographicDataAsync(
            Gender? gender,
            int? minAge,
            int? maxAge,
            Priority? priority,
            string? ageRange,
            int reportTypeId);
        Task<List<HeatMapData>> GetHeatmapDataAsync(string? priority, int? reportTypeId);
        Task<List<IncidentRateData>> GetIncidentRateDataAsync(string? timeRange, string? location, int? reportTypeId, string? dateFilterType, DateTime? startDate, DateTime? endDate);

    }
}