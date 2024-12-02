using Microsoft.AspNetCore.Mvc;
using prasApi.Interfaces;
using prasApi.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace prasApi.Controllers
{
    [ApiController]
    [Route("api/incident")]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentRepository _incidentRepository;

        // Constructor to inject the IncidentRepository dependency
        public IncidentController(IIncidentRepository incidentRepository)
        {
            _incidentRepository = incidentRepository;
        }

        // Endpoint to get demographic data
        [HttpGet("demographic-data")]
        public async Task<IActionResult> GetDemographicData(
            string? gender,
            int? minAge,
            int? maxAge,
            string? priority,
            string? ageRange,
            int reportTypeId) // Added reportTypeId as a required parameter
        {
            try
            {
                var demographicData = await _incidentRepository.GetDemographicDataAsync(
                    gender,
                    minAge,
                    maxAge,
                    priority,
                    ageRange,
                    reportTypeId // Pass the reportTypeId
                );
                return Ok(demographicData);
            }
            catch (Exception ex)
            {
                // Log the exception
                return StatusCode(500, "An error occurred while retrieving demographic data: " + ex.Message);
            }
        }

        // Endpoint to get heatmap data
        [HttpGet("heatmap")]
        public async Task<IActionResult> GetHeatmapData(
            [FromQuery] string? priority,
            [FromQuery] string? reportTypeId)  // Changed to string for flexibility
        {
            try
            {
                // Initialize reportTypeId to null for flexibility
                int? reportTypeIdInt = null;

                // If reportTypeId is provided and it's not "all", try to parse it
                if (!string.IsNullOrEmpty(reportTypeId))
                {
                    if (reportTypeId.Equals("all", StringComparison.OrdinalIgnoreCase))
                    {
                        reportTypeIdInt = null; // "all" means no filter
                    }
                    else if (int.TryParse(reportTypeId, out int parsedReportTypeId))
                    {
                        reportTypeIdInt = parsedReportTypeId; // Valid integer, use it
                    }
                    else
                    {
                        return BadRequest("Invalid reportTypeId provided.");
                    }
                }

                // Get heatmap data based on priority and report type
                var heatmapData = await _incidentRepository.GetHeatmapDataAsync(priority, reportTypeIdInt);

                // Return the data if found
                if (heatmapData == null || heatmapData.Count == 0)
                {
                    return NotFound("No heatmap data found for the provided filters.");
                }

                return Ok(heatmapData);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an internal server error
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        // Endpoint to get incident rate data
        [HttpGet("incident-rates")]
        public async Task<IActionResult> GetIncidentRateData(
            [FromQuery] string? state,
            [FromQuery] int? reportTypeId,  // Made reportTypeId optional (int?)
            [FromQuery] string? priority,
            [FromQuery] string? timeRange,  // Added timeRange parameter
            [FromQuery] DateTime? startDate,
            [FromQuery] DateTime? endDate)
        {
            try
            {
                // Get incident rate data based on the provided filters, including timeRange
                var incidentRateData = await _incidentRepository.GetIncidentRateDataAsync(
                    timeRange, state, reportTypeId, priority, startDate, endDate);

                // Return the data if found
                if (incidentRateData == null || incidentRateData.Count == 0)
                {
                    return NotFound("No incident rate data found for the provided filters.");
                }

                return Ok(incidentRateData);
            }
            catch (Exception ex)
            {
                // Handle any exceptions and return an internal server error
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

    }
}
