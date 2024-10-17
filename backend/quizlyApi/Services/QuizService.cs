using quizlyApi.Data;
using quizlyApi.Models;
using Microsoft.EntityFrameworkCore;

namespace quizlyApi.Services
{
    public class QuizService : IQuizService
    {
        private readonly QuizlyDbContext _context;

        public QuizService(QuizlyDbContext context)
        {
            _context = context;
        }

        public async Task<QuizContent?> GetByIdAsync(int quizId, int userId)
        {
            return await _context.QuizContents
                .Include(qc => qc.QuizConfig)
                .Include(qc => qc.User)
                .FirstOrDefaultAsync(qc => qc.QuizId == quizId && qc.UserId == userId);
        }

        public async Task<IEnumerable<QuizContent>> GetByUserIdAsync(int userId)
        {
            return await _context.QuizContents
                .Include(qc => qc.QuizConfig)
                .Where(qc => qc.UserId == userId)
                .ToListAsync();
        }

        public async Task<QuizContent> CreateAsync(QuizContent quizContent)
        {
            _context.QuizContents.Add(quizContent);
            await _context.SaveChangesAsync();
            return quizContent;
        }

        public async Task<bool> UpdateAsync(QuizContent quizContent)
        {
            _context.Entry(quizContent).State = EntityState.Modified;
            try
            {
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await ExistsAsync(quizContent.QuizId, quizContent.UserId))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int quizId, int userId)
        {
            var quizContent = await _context.QuizContents.FindAsync(quizId, userId);
            if (quizContent == null)
            {
                return false;
            }

            _context.QuizContents.Remove(quizContent);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> ExistsAsync(int quizId, int userId)
        {
            return await _context.QuizContents.AnyAsync(qc => qc.QuizId == quizId && qc.UserId == userId);
        }
    }
}