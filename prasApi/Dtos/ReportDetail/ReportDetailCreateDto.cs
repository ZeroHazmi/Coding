using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace prasApi.Dtos.ReportDetail
{
    public class ReportDetailCreateDto
    {
        public int ReportTypeId { get; set; }
        public string FieldValue { get; set; } = string.Empty;
        public string Audio { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
    }
}