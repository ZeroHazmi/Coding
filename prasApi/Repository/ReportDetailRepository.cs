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
    public class ReportDetailRepository : IReportDetailRepository
    {
        private readonly ApplicationDbContext _context;
        public ReportDetailRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ReportDetail> CreateAsync(ReportDetail reportDetail)
        {
            await _context.ReportDetail.AddAsync(reportDetail);
            await _context.SaveChangesAsync();
            return reportDetail;
        }

        public async Task<ReportDetail?> DeleteAsync(int id)
        {
            var reportDetail = await _context.ReportDetail.FindAsync(id);
            if (reportDetail == null)
            {
                return null;
            }

            _context.ReportDetail.Remove(reportDetail);
            await _context.SaveChangesAsync();
            return reportDetail;
        }

        public async Task<List<ReportDetail>> GetAllAsync()
        {
            return await _context.ReportDetail.ToListAsync();
        }

        public Task<ReportDetail?> GetByIdAsync(int id)
        {
            return _context.ReportDetail.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<ReportDetail?> UpdateAsync(int id, ReportDetail reportDetail)
        {
            var existingReportDetail = await _context.ReportDetail.FindAsync(reportDetail.Id);
            if (existingReportDetail == null)
            {
                return null;
            }

            return existingReportDetail;

        }
    }
}