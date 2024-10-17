using quizlyApi.Models;

namespace quizlyApi.Services
{
    public interface IQuizConfigService
    {
        Task<QuizConfig?> GetByIdAsync(int id);
        Task<IEnumerable<QuizConfig>> GetAllAsync();
        Task<QuizConfig> CreateAsync(QuizConfig quizConfig);
        Task<bool> UpdateAsync(QuizConfig quizConfig);
        Task<bool> DeleteAsync(int id);
        Task<bool> ExistsAsync(int id);
    }
}