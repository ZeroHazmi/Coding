using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prasApi.Dtos.Report;
using prasApi.Models;

namespace prasApi.Mappers
{
    public static class ReportMapper
    {
        public static ReportDto ToReportDto(this Report report)
        {
            return new ReportDto
            {
                Id = report.Id,
                UserId = report.UserId,
                ReportTypeId = report.ReportTypeId,
                ReportDetailId = report.ReportDetailId,
                Status = report.Status,
                Priority = report.Priority
            };
        }
    }
}