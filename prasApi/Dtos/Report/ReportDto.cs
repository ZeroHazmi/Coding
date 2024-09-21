using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prasApi.Models;

namespace prasApi.Dtos.Report
{
    public class ReportDto
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int ReportTypeId { get; set; }
        public int ReportDetailId { get; set; }
        public Status Status { get; set; } = Status.Open;
        public Priority Priority { get; set; } = Priority.Low;
    }
}