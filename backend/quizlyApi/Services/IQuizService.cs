using quizlyApi.Models;

namespace quizlyApi.Services
{
    public interface IQuizService
    {
        Task<QuizContent?> GetByIdAsync(int quizId, int userId);
        Task<IEnumerable<QuizContent>> GetByUserIdAsync(int userId);
        Task<QuizContent> CreateAsync(QuizContent quizContent);
        Task<bool> UpdateAsync(QuizContent quizContent);
        Task<bool> DeleteAsync(int quizId, int userId);
        Task<bool> ExistsAsync(int quizId, int userId);
    }
}