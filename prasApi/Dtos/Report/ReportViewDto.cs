using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prasApi.Dtos.Report
{
    public class ReportViewDto
    {
        public string ReportId { get; set; } = string.Empty;
        public DateTime DateCreated { get; set; }
        public string Status { get; set; } = string.Empty;
        public string Priority { get; set; } = string.Empty;
        public DateTime IncidentDateTime { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Transcript { get; set; } = string.Empty;
        public string PoliceNotes { get; set; } = string.Empty;
    }
}