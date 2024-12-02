using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.ObjectPool;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using prasApi.Dtos.Report;
using prasApi.Interfaces;
using prasApi.Mappers;
using prasApi.Models;

namespace prasApi.Controllers
{
    [ApiController]
    [Route("api/report")]
    public class ReportController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;
        private readonly IReportDetailRepository _reportDetailRepository;
        private readonly UserManager<AppUser> _userManager;

        public ReportController(IReportRepository reportRepository, IReportDetailRepository reportDetailRepository, UserManager<AppUser> userManager)
        {
            _reportRepository = reportRepository;
            _reportDetailRepository = reportDetailRepository;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? search,
            [FromQuery] string? userId,
            [FromQuery] string? policeId,
            [FromQuery] string? status = null,
            [FromQuery] string? priority = null,
            [FromQuery] string sortOrder = "asc")
        {
            Status? parsedStatus = null;
            Priority? parsedPriority = null;

            if (!string.IsNullOrEmpty(status) && Enum.TryParse(status, true, out Status statusValue))
            {
                parsedStatus = statusValue;
            }

            if (!string.IsNullOrEmpty(priority) && Enum.TryParse(priority, true, out Priority priorityValue))
            {
                parsedPriority = priorityValue;
            }

            var reports = await _reportRepository.GetAllAsync(search, userId, policeId, parsedStatus, parsedPriority, sortOrder);

            if (!string.IsNullOrEmpty(policeId))
            {
                var policeReportDtos = reports
                    .Where(r => r.AppUserId == policeId)
                    .Select(x => x.ToReportPoliceDto())
                    .Where(dto => dto != null)
                    .ToList();
                return Ok(policeReportDtos);
            }

            if (!string.IsNullOrEmpty(userId))
            {
                var userReportDtos = reports
                    .Where(r => r.UserId == userId)
                    .Select(x => x.ToUserReportDto())
                    .Where(dto => dto != null)
                    .ToList();
                return Ok(userReportDtos);
            }

            return Ok(new List<ReportUserDto>()); // Return empty if no filters match
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var report = await _reportRepository.GetByIdAsync(id);
            if (report == null)
            {
                return NotFound();
            }
            var reportViewDto = new ReportViewDto
            {
                ReportId = report.Id,
                ReportTypeName = report.ReportType?.Name ?? "Unknown",
                DateCreated = report.CreatedAt,
                Status = report.Status,
                Priority = report.Priority,
                IncidentDate = report.ReportDetail.Date,
                IncidentTime = report.ReportDetail.Time,
                Location = report.ReportDetail.Address,
                Transcript = report.ReportDetail.Transcript,
                ExtraInformation = report.ReportDetail.ExtraInformation
            };

            return Ok(reportViewDto);
        }

        [Authorize(Roles = "User, Police, Admin")]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ReportCreateDto reportCreateDto)
        {
            try
            {
                if (reportCreateDto == null || reportCreateDto.ReportDetail == null)
                {
                    return BadRequest("Invalid report data.");
                }

                // Log received DTO
                Console.WriteLine($"ReportCreateDto: {JsonConvert.SerializeObject(reportCreateDto)}");

                // Assign random police officer
                var policeOfficers = await _userManager.GetUsersInRoleAsync("Police");
                if (policeOfficers.Count == 0)
                {
                    return StatusCode(StatusCodes.Status500InternalServerError, "No police officers available.");
                }

                string assignedOfficerId = policeOfficers[new Random().Next(policeOfficers.Count)].Id;

                var reportDetail = new ReportDetail
                {
                    ReportTypeId = reportCreateDto.ReportDetail.ReportTypeId,
                    Date = reportCreateDto.ReportDetail.Date,
                    Time = reportCreateDto.ReportDetail.Time,
                    Address = reportCreateDto.ReportDetail.Address,
                    Latitude = reportCreateDto.ReportDetail.Latitude,
                    Longitude = reportCreateDto.ReportDetail.Longitude,
                    State = reportCreateDto.ReportDetail.State,
                    FieldValue = reportCreateDto.ReportDetail.FieldValue,
                    Audio = reportCreateDto.ReportDetail.Audio,
                    Image = reportCreateDto.ReportDetail.Image,
                    Transcript = reportCreateDto.ReportDetail.Transcript
                };

                var createdReportDetail = await _reportDetailRepository.CreateAsync(reportDetail);

                if (createdReportDetail == null)
                {
                    throw new Exception("Failed to create report detail.");
                }

                var report = new Report
                {
                    UserId = reportCreateDto.UserId,
                    ReportTypeId = reportCreateDto.ReportTypeId,
                    Status = reportCreateDto.Status,
                    Priority = reportCreateDto.Priority,
                    AppUserId = assignedOfficerId,
                    ReportDetailId = createdReportDetail.Id,
                };

                var createdReport = await _reportRepository.CreateAsync(report);

                return CreatedAtAction(nameof(GetById), new { id = createdReport.Id }, createdReport);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while creating the report.");
            }
        }


        [Authorize(Roles = "Police")]
        [HttpPut("put")]
        public async Task<IActionResult> Update([FromQuery] int id, [FromBody] ReportUpdateDto updateReportDto)
        {
            // Fetch the existing report by id
            var reportExist = await _reportRepository.GetByIdAsync(id);
            if (reportExist == null)
            {
                return NotFound(); // Report not found
            }

            // Update the Report fields
            reportExist.Status = updateReportDto.Status;
            reportExist.Priority = updateReportDto.Priority;

            // If ExtraInformation is provided, update it
            if (!string.IsNullOrEmpty(updateReportDto.ExtraInformation))
            {
                reportExist.ReportDetail.ExtraInformation = updateReportDto.ExtraInformation;
            }

            // Call the repository's update method to save changes
            var updatedReport = await _reportRepository.UpdateAsync(id, reportExist);

            // Return the updated report
            return Ok(updatedReport);
        }

        [Authorize(Roles = "Admin, Police")]
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // Check if the report exists
            var reportExist = await _reportRepository.GetByIdAsync(id);
            if (reportExist == null)
            {
                return NotFound();
            }

            // Perform the deletion
            await _reportRepository.DeleteAsync(id);
            return NoContent();
        }
    }
}