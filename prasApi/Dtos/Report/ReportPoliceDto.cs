using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prasApi.Models;

namespace prasApi.Dtos.Report
{
    public class ReportPoliceDto
    {

        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public string ReportTypeName { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public string Name { get; set; } = string.Empty;
        public string IcNumber { get; set; } = string.Empty;
        public Status Status { get; set; } = Status.Open;
        public Priority Priority { get; set; } = Priority.Low;
    }
}