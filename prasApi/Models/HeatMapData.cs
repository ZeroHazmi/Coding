using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prasApi.Models
{
    public class HeatMapData
    {
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public int ReportTypeId { get; set; }
    }
}