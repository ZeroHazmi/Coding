using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using prasApi.Models;

namespace prasApi.Interfaces
{
    public interface IFeedbackRepository
    {
        Task<List<Feedback>> GetAllAsync();
        Task<Feedback?> GetByIdAsync(int id);
        Task<Feedback> CreateAsync(Feedback feedback);
        Task<Feedback?> UpdateAsync(int id, Feedback feedback);
        Task<Feedback?> DeleteAsync(int id);
    }
}