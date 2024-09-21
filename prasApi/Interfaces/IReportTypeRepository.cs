using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prasApi.Models;

namespace prasApi.Interfaces
{
    public interface IReportTypeRepository
    {
        Task<List<ReportType>> GetAllAsync();
        Task<ReportType?> GetByIdAsync(int id);
        Task<ReportType> CreateAsync(ReportType reportType);
        Task<ReportType?> UpdateAsync(int id, ReportType reportType);
        Task<ReportType?> DeleteAsync(int id);
    }
}