using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prasApi.Models
{
    public class IncidentRateData
    {
        public int ReportTypeId { get; set; }
        public string ReportTypeName { get; set; } = string.Empty;
        public int IncidentCount { get; set; }
    }
}