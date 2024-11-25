using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prasApi.Models
{
    public class DemographicData
    {
        public string Gender { get; set; }
        public int Age { get; set; }
        public int ReportTypeId { get; set; }
        public int IncidentCount { get; set; }
    }
}