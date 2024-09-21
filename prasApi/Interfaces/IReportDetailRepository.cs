using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prasApi.Models;

namespace prasApi.Interfaces
{
    public interface IReportDetailRepository
    {
        Task<List<ReportDetail>> GetAllAsync();
        Task<ReportDetail?> GetByIdAsync(int id);
        Task<ReportDetail> CreateAsync(ReportDetail reportDetail);
        Task<ReportDetail?> UpdateAsync(int id, ReportDetail reportDetail);
        Task<ReportDetail?> DeleteAsync(int id);
    }
}