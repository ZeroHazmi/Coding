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

        // Navigation Properties
        public ReportDetailCreateDto ReportDetail { get; set; }
    }
}