using quizlyApi.Data;
using quizlyApi.Models;
using Microsoft.EntityFrameworkCore;

namespace quizlyApi.Services
{
    public class QuizConfigService : IQuizConfigService
    {
        private readonly QuizlyDbContext _context;

        public QuizConfigService(QuizlyDbContext context)
        {
            _context = context;
        }

        public async Task<QuizConfig?> GetByIdAsync(int id)
        {
            return await _context.QuizConfigs.FindAsync(id);
        }

        public async Task<IEnumerable<QuizConfig>> GetAllAsync()
        {
            return await _context.QuizConfigs.ToListAsync();
        }

        public async Task<QuizConfig> CreateAsync(QuizConfig quizConfig)
        {
            _context.QuizConfigs.Add(quizConfig);
            await _context.SaveChangesAsync();
            return quizConfig;
        }

        public async Task<bool> UpdateAsync(QuizConfig quizConfig)
        {
            _context.Entry(quizConfig).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ExistsAsync(quizConfig.Id))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var quizConfig = await _context.QuizConfigs.FindAsync(id);
            if (quizConfig == null)
            {
                return false;
            }

            _context.QuizConfigs.Remove(quizConfig);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int id)
        {
            return await _context.QuizConfigs.AnyAsync(e => e.Id == id);
        }
    }
}