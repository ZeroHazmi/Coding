using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prasApi.Models;

namespace prasApi.Interfaces
{
    public interface IReportRepository
    {
        Task<List<Report>> GetAllAsync(Status? status = null, Priority? priority = null, DateTime? createdDate = null, string sortOrder = "asc");
        Task<Report?> GetByIdAsync(int id);
        Task<Report> CreateAsync(Report report);
        Task<Report?> UpdateAsync(int id, Report report);
        Task<Report?> DeleteAsync(int id);
    }
}