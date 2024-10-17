using quizlyApi.Models;

namespace quizlyApi.DTOs;

public class QuizDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public Difficulty Difficulty { get; set; }
    public string RawContent { get; set; }
    public string? PostProcessedContent { get; set; }
}

public class AnswerQuizDto
{
    public int? UserId { get; set; }
    public int? QuizId { get; set; }

    public List<AnswerQuizQAPair> Answers { get; set; }
}

public class AnswerQuizQAPair
{
    public int ProbelmId { get; set; }
    public string Answer { get; set; }
}