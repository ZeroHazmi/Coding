using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prasApi.Models
{
    public class DemographicData
    {
        public string Gender { get; set; } = string.Empty;
        public int Age { get; set; }
        public Priority Priority { get; set; }
        public int ReportTypeId { get; set; }
        public string ReportTypeName { get; set; } = string.Empty;
        public int IncidentCount { get; set; }
    }
}