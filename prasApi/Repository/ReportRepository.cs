using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using prasApi.Data;
using prasApi.Interfaces;
using prasApi.Models;

namespace prasApi.Repository
{
    public class ReportRepository : IReportRepository
    {
        private readonly ApplicationDbContext _context;
        public ReportRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Report> CreateAsync(Report report)
        {
            await _context.Reports.AddAsync(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<Report?> DeleteAsync(int id)
        {
            var report = await _context.Reports.FindAsync(id);
            if (report == null)
            {
                return null;
            }

            _context.Reports.Remove(report);
            await _context.SaveChangesAsync();
            return report;
        }

        public async Task<List<Report>> GetAllAsync(Status? status = null, Priority? priority = null, DateTime? createdDate = null, string sortOrder = "asc")
        {
            // Start with the base query
            var query = _context.Reports.AsQueryable();

            // Apply filters based on provided parameters
            if (status.HasValue)
            {
                query = query.Where(r => r.Status == status.Value);
            }

            if (priority.HasValue)
            {
                query = query.Where(r => r.Priority == priority.Value);
            }

            // Apply sorting by CreatedAt in ascending or descending order
            if (sortOrder.ToLower() == "desc")
            {
                query = query.OrderByDescending(r => r.CreatedAt);
            }
            else
            {
                query = query.OrderBy(r => r.CreatedAt);
            }

            // Execute the query and return the filtered list
            return await query.ToListAsync();
        }

        public async Task<Report?> GetByIdAsync(int id)
        {
            return await _context.Reports.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Report?> UpdateAsync(int id, Report report)
        {
            var existingReport = await _context.Reports.FindAsync(report.Id);
            if (existingReport == null)
            {
                return null;
            }

            return existingReport;
        }
    }
}