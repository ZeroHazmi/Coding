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
        public async Task<IActionResult> GetAll([FromQuery] string status = null)
        {
            // Retrieve all reports
            var reports = await _reportRepository.GetAllAsync();

            // If status query parameter is provided, filter reports by the specified status
            if (!string.IsNullOrEmpty(status))
            {
                reports = reports.Where(r => string.Equals(r.Status.ToString(), status, StringComparison.OrdinalIgnoreCase)).ToList();
            }

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

            // Get the current logged-in user's username from claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            var reportDetail = new ReportDetail
            {
                ReportTypeId = reportCreateDto.ReportDetail.ReportTypeId,
                Date = reportCreateDto.ReportDetail.Date,
                Location = reportCreateDto.ReportDetail.Location,
                Time = reportCreateDto.ReportDetail.Time,
                FieldValue = reportCreateDto.ReportDetail.FieldValue,
                Audio = reportCreateDto.ReportDetail.Audio,
                Image = reportCreateDto.ReportDetail.Image,
                Transcript = reportCreateDto.ReportDetail.Transcript
            };

            var createdReportDetail = await _reportDetailRepository.CreateAsync(reportDetail);

            //Create the ReportType object
            var report = new Report
            {
                UserId = userId,
                ReportTypeId = reportCreateDto.ReportTypeId,
                Status = reportCreateDto.Status,
                Priority = reportCreateDto.Priority,
                AppUserId = userId,
                ReportDetailId = createdReportDetail.Id
            };

            var createdReport = await _reportRepository.CreateAsync(report);
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