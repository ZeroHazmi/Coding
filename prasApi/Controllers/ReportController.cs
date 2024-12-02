using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
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
            [FromQuery] string? status = null,
            [FromQuery] string? priority = null,
            [FromQuery] string sortOrder = "asc")
        {
            // Parse the status and priority strings into enums if they are provided
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

            // Retrieve all reports based on the filters and sort order
            var reports = await _reportRepository.GetAllAsync(search, userId, parsedStatus, parsedPriority, sortOrder);

            // Map the filtered reports to DTOs
            var reportDtos = reports.Select(x => x.ToReportDto()).ToList();

            // If userId is provided, return user-specific report DTOs
            if (!string.IsNullOrEmpty(userId))
            {
                var userReportDto = reports
                    .Select(x => x.ToUserReportDto())
                    .Where(dto => dto != null) // Filter out any null DTOs
                    .ToList();
                return Ok(userReportDto);
            }

            // Return the general report DTOs
            return Ok(reportDtos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reportType = await _reportRepository.GetByIdAsync(id);
            if (reportType == null)
            {
                return NotFound();
            }

            return Ok(reportType);
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


        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReportDto reportDto)
        {
            var reportExist = await _reportRepository.GetByIdAsync(id);
            if (reportExist == null)
            {
                return NotFound();
            }

            //Update the ReportType object
            var report = new Report
            {
                Id = id,
                UserId = reportDto.UserId,
                ReportTypeId = reportDto.ReportTypeId,
                ReportDetailId = reportDto.ReportDetailId,
                Status = reportDto.Status,
                Priority = reportDto.Priority
            };

            var updatedReportType = await _reportRepository.UpdateAsync(id, report);
            return Ok(updatedReportType);
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