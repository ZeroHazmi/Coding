using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prasApi.Models
{
    public class IncidentRateData
    {
        public DateTime Date { get; set; }
        public int ReportTypeId { get; set; }
        public int IncidentCount { get; set; }
    }
}