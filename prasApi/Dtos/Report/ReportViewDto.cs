using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prasApi.Models;

namespace prasApi.Dtos.Report
{
    public class ReportViewDto
    {
        public int ReportId { get; set; }
        public string ReportTypeName { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public DateTime IncidentDate { get; set; }
        public string Location { get; set; } = string.Empty;
        public string IncidentTime { get; set; } = string.Empty;
        public string Transcript { get; set; } = string.Empty;
        public string ExtraInformation { get; set; } = string.Empty;
    }
}