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
        public static ReportUserDto ToUserReportDto(this Report report)
        {
            // Add a null check for the report itself
            if (report == null)
            {
                return null;
            }

            return new ReportUserDto
            {
                Id = report.Id,
                UserId = report.UserId,
                ReportId = report.Id,
                ReportTypeName = report.ReportType?.Name ?? "Unknown",
                CreateAt = report.CreatedAt,
                Status = report.Status,
            };
        }
    }
}