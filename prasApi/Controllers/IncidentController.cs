using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using prasApi.Interfaces;

namespace prasApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IncidentController : ControllerBase
    {
        private readonly IReportRepository _reportRepository;
        private readonly IReportDetailRepository _reportDetailRepository;

        public IncidentController(IReportRepository reportRepository, IReportDetailRepository reportDetailRepository)
        {
            _reportRepository = reportRepository;
            _reportDetailRepository = reportDetailRepository;
        }
    }
}