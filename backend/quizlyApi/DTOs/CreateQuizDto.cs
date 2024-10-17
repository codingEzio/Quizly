using quizlyApi.Models;

namespace quizlyApi.DTOs;

public class CreateQuizDto
{
    public int userId { get; set; }
    public string title { get; set; }
    public string context { get; set; }

    public Language? language { get; set; }
    public Difficulty? difficulty { get; set; }
}