using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prasApi.Models;

namespace prasApi.Dtos.Report
{
    public class ReportUserDto
    {
        public int Id { get; set; }
        public int ReportId { get; set; }
        public string ReportTypeName { get; set; } = string.Empty;
        public DateTime CreateAt { get; set; }
        public string UserId { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.Open;
    }
}