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
        public async Task<IActionResult> GetAll()
        {
            var reportTypes = await _reportRepository.GetAllAsync();
            var reportTypeDto = reportTypes.Select(x => x.ToReportDto());
            return Ok(reportTypeDto);
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

        [Authorize]
        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ReportCreateDto reportCreateDto)
        {

            // Get the current logged-in user's username from claims
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                return Unauthorized();
            }

            //Console.WriteLine($"User ID: {userId}");

            var reportDetail = new ReportDetail
            {
                ReportTypeId = reportCreateDto.ReportDetail.ReportTypeId,
                FieldValue = reportCreateDto.ReportDetail.FieldValue,
                Audio = reportCreateDto.ReportDetail.Audio,
                Image = reportCreateDto.ReportDetail.Image
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


    }
}