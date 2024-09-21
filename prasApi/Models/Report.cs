using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prasApi.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string UserId { get; set; } = string.Empty;
        public int ReportTypeId { get; set; }
        public int ReportDetailId { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Status Status { get; set; } = Status.Open;
        public Priority Priority { get; set; } = Priority.Low;
        public string AppUserId { get; set; } = string.Empty;

        // Navigation Properties
        public AppUser AppUser { get; set; }
        public ReportType ReportType { get; set; }
        public ReportDetail ReportDetail { get; set; }
    }
}