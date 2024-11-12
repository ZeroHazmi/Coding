using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using prasApi.Dtos.ReportDetail;
using prasApi.Models;

namespace prasApi.Dtos.Report
{
    public class ReportCreateDto
    {
        [Required]
        public int ReportTypeId { get; set; }
        [Required]
        public Status Status { get; set; } = Status.Open;
        [Required]
        public Priority Priority { get; set; } = Priority.Low;
        public string AppUserId { get; set; } = string.Empty; // Police officer ID

        // Nullable IC number for unregistered users
        public string? IcNumber { get; set; }

        // Navigation Properties
        public ReportDetailCreateDto ReportDetail { get; set; }
    }
}