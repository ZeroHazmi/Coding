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
    public class ReportTypeRepository : IReportTypeRepository
    {
        private readonly ApplicationDbContext _context;
        public ReportTypeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReportType> CreateAsync(ReportType reportType)
        {
            await _context.ReportTypes.AddAsync(reportType);
            await _context.SaveChangesAsync();
            return reportType;
        }

        public async Task<ReportType?> DeleteAsync(int id)
        {
            var reportType = await _context.ReportTypes.FindAsync(id);
            if (reportType == null)
            {
                return null;
            }

            _context.ReportTypes.Remove(reportType);
            await _context.SaveChangesAsync();
            return reportType;
        }

        public async Task<List<ReportType>> GetAllAsync(bool? isOnline = null)
        {
            var query = _context.ReportTypes.AsQueryable();

            // Apply filter if isOnline is specified
            if (isOnline.HasValue)
            {
                query = query.Where(rt => rt.IsOnlineAllowed == isOnline.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<ReportType?> GetByIdAsync(int id)
        {
            return await _context.ReportTypes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ReportType?> UpdateAsync(int id, ReportType reportType)
        {
            var existingReportType = _context.ReportTypes.Find(id);
            if (existingReportType == null)
            {
                return null;
            }

            existingReportType.Name = reportType.Name;
            existingReportType.Description = reportType.Description;
            existingReportType.TemplateStructure = reportType.TemplateStructure;

            await _context.SaveChangesAsync();
            return existingReportType;
        }
    }
}