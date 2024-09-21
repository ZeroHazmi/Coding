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
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ApplicationDbContext _context;
        public FeedbackRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Feedback> CreateAsync(Feedback feedback)
        {
            await _context.Feedback.AddAsync(feedback);
            await _context.SaveChangesAsync();
            return feedback;
        }

        public async Task<Feedback?> DeleteAsync(int id)
        {
            var feedback = await _context.Feedback.FindAsync(id);
            if (feedback == null)
            {
                return null;
            }

            _context.Feedback.Remove(feedback);
            await _context.SaveChangesAsync();
            return feedback;
        }

        public async Task<List<Feedback>> GetAllAsync()
        {
            return await _context.Feedback.ToListAsync();
        }

        public async Task<Feedback?> GetByIdAsync(int id)
        {
            return await _context.Feedback.FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Feedback?> UpdateAsync(int id, Feedback feedback)
        {
            var existingFeedback = await _context.Feedback.FindAsync(feedback.Id);
            if (existingFeedback == null)
            {
                return null;
            }

            return existingFeedback;
        }
    }
}