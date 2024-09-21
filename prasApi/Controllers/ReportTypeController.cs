using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using prasApi.Dtos.ReportType;
using prasApi.Interfaces;
using prasApi.Mappers;
using prasApi.Models;

namespace prasApi.Controllers
{
    [ApiController]
    [Route("api/reporttype")]
    public class ReportTypeController : ControllerBase
    {
        private readonly IReportTypeRepository _reportTypeRepository;
        public ReportTypeController(IReportTypeRepository reportTypeRepository)
        {
            _reportTypeRepository = reportTypeRepository;
        }


        // Not finished - need to add parameters so that the user can filter the report types
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reportTypes = await _reportTypeRepository.GetAllAsync();
            var reportTypeDto = reportTypes.Select(x => x.ToReportTypeDto());
            return Ok(reportTypeDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var reportType = await _reportTypeRepository.GetByIdAsync(id);
            if (reportType == null)
            {
                return NotFound();
            }

            return Ok(reportType);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] ReportTypeCreateDto reportTypeCreateDto)
        {
            // Serialize the TemplateStructure Property to JSON
            var serializedTemplateStructure = JsonSerializer.Serialize(reportTypeCreateDto.TemplateStructure);

            //Create the ReportType object
            var reportType = new ReportType
            {
                Name = reportTypeCreateDto.Name,
                Description = reportTypeCreateDto.Description,
                TemplateStructure = serializedTemplateStructure
            };

            var createdReportType = await _reportTypeRepository.CreateAsync(reportType);
            return CreatedAtAction(nameof(GetById), new { id = createdReportType.Id }, createdReportType);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ReportTypeUpdateDto reportTypeUpdateDto)
        {
            var reportType = await _reportTypeRepository.GetByIdAsync(id);
            if (reportType == null)
            {
                return NotFound();
            }

            // Serialize the TemplateStructure Property to JSON
            var serializedTemplateStructure = JsonSerializer.Serialize(reportTypeUpdateDto.TemplateStructure);

            //Update the ReportType object
            reportType.Name = reportTypeUpdateDto.Name;
            reportType.Description = reportTypeUpdateDto.Description;
            reportType.TemplateStructure = serializedTemplateStructure;

            var updatedReportType = await _reportTypeRepository.UpdateAsync(id, reportType);
            return Ok(updatedReportType);
        }
    }
}