using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prasApi.Dtos.Incident
{
    public class IncidentReportTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int IncidentCount { get; set; }
    }
}