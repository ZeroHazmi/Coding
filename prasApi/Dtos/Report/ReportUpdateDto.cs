using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prasApi.Models;

namespace prasApi.Dtos.Report
{
    public class ReportUpdateDto
    {
        public Status Status { get; set; }
        public Priority Priority { get; set; }
        public string ExtraInformation { get; set; } = string.Empty;
    }
}