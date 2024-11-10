using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prasApi.Models
{
    public class ReportDetail
    {
        public int Id { get; set; }
        public int ReportTypeId { get; set; }
        public DateTime Date { get; set; }
        public string Time { get; set; } = string.Empty;
        public string Location { get; set; } = string.Empty;
        public string FieldValue { get; set; } = string.Empty; // Change sql datatype to json in DBContext
        public string Audio { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public string Transcript { get; set; } = string.Empty;

        // Navigation Properties
        public ReportType ReportType { get; set; }
    }
}