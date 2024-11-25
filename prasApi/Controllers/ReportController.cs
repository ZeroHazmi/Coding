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
            [FromQuery] string? status = null,
            [FromQuery] string? priority = null,
            [FromQuery] DateTime? createdDate = null,
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
            var reports = await _reportRepository.GetAllAsync(parsedStatus, parsedPriority, createdDate, sortOrder);

            // Map the filtered reports to DTOs
            var reportDtos = reports.Select(x => x.ToReportDto());

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
            // Validate the incoming DTO
            if (reportCreateDto == null || reportCreateDto.ReportDetail == null)
            {
                return BadRequest("Invalid report data.");
            }

            // Get the current logged-in user's username from claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Get user ID from the JWT token

            // Get the user's roles from claims
            var roles = User.FindAll(ClaimTypes.Role).Select(c => c.Value).ToList();

            // Retrieve all users in the "Police" role
            var policeOfficers = await _userManager.GetUsersInRoleAsync("Police");

            if (policeOfficers.Count == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "No police officers available to assign.");
            }

            // Assign a random police officer to handle the report
            string assignedOfficerId = policeOfficers[new Random().Next(policeOfficers.Count)].Id;

            // Initialize the IC number
            string icNumber = null;

            if (roles.Contains("Police"))
            {
                // If a police officer is submitting the report, the IC number may be required
                if (string.IsNullOrWhiteSpace(reportCreateDto.IcNumber))
                {
                    return BadRequest("IC number is required for reports submitted at the police station.");
                }
                else
                {
                    icNumber = reportCreateDto.IcNumber ?? string.Empty;
                }
            }
            else if (roles.Contains("User"))
            {
                // If the user is submitting the report, assign IC number if provided
                icNumber = reportCreateDto.IcNumber;
            }

            // Create the ReportDetail from DTO
            var reportDetail = new ReportDetail
            {
                ReportTypeId = reportCreateDto.ReportDetail.ReportTypeId,
                Date = reportCreateDto.ReportDetail.Date,
                Time = reportCreateDto.ReportDetail.Time,
                Address = reportCreateDto.ReportDetail.Address,  // Ensure Address is passed in DTO
                Latitude = reportCreateDto.ReportDetail.Latitude,
                Longitude = reportCreateDto.ReportDetail.Longitude,
                State = reportCreateDto.ReportDetail.State,
                FieldValue = reportCreateDto.ReportDetail.FieldValue,
                Audio = reportCreateDto.ReportDetail.Audio,
                Image = reportCreateDto.ReportDetail.Image,
                Transcript = reportCreateDto.ReportDetail.Transcript
            };

            // Create the ReportDetail in the database
            var createdReportDetail = await _reportDetailRepository.CreateAsync(reportDetail);

            // Create the Report object from the DTO and associate with created ReportDetail
            var report = new Report
            {
                UserId = userId,  // User ID (could be null for anonymous users)
                ReportTypeId = reportCreateDto.ReportTypeId,
                Status = reportCreateDto.Status,
                Priority = reportCreateDto.Priority,
                AppUserId = assignedOfficerId,  // The officer handling the report
                ReportDetailId = createdReportDetail.Id,
                IcNumber = icNumber  // Set the IC number if available
            };

            // Create the Report in the database
            var createdReport = await _reportRepository.CreateAsync(report);

            // Return the created report object with a CreatedAtAction response
            return CreatedAtAction(nameof(GetById), new { id = createdReport.Id }, createdReport);
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