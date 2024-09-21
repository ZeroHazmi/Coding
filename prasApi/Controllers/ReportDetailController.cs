using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using prasApi.Interfaces;

namespace prasApi.Controllers
{
    [ApiController]
    [Route("api/repordetail")]
    public class ReportDetailController : ControllerBase
    {
        private readonly IReportDetailRepository _reportDetailRepository;
        public ReportDetailController(IReportDetailRepository reportDetailRepository)
        {
            _reportDetailRepository = reportDetailRepository;
        }    
        
    }
}