using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prasApi.Dtos.ReportType;
using prasApi.Models;

namespace prasApi.Mappers
{
    public static class ReportTypeMappers
    {
        public static ReportTypeDto ToReportTypeDto(this ReportType reportType)
        {
            return new ReportTypeDto
            {
                Id = reportType.Id,
                Name = reportType.Name,
                Description = reportType.Description
            };
        }
    }
}