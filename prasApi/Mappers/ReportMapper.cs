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
            return new ReportUserDto
            {
                Id = report.Id,
                ReportTypeName = report.ReportType.Name,
                CreateAt = report.CreatedAt,
                Status = report.Status,
            };

        }

        public static ReportPoliceDto ToReportPoliceDto(this Report report)
        {
            return new ReportPoliceDto
            {
                Id = report.Id,
                UserId = report.UserId,
                ReportTypeName = report.ReportType.Name,
                Name = report.AppUser.Name,
                IcNumber = report.AppUser.IcNumber,
                DateCreated = report.CreatedAt,
                Status = report.Status,
                Priority = report.Priority
            };
        }
    }
}
