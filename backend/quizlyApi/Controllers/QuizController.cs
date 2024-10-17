using Microsoft.AspNetCore.Mvc;
using quizlyApi.Services;
using quizlyApi.DTOs;
using quizlyApi.Providers;

namespace quizlyApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class QuizController : ControllerBase
    {
        private readonly IQuizService _quizService;
        private readonly QuizProvider _quizProvider;

        public QuizController(IQuizService quizService, QuizProvider quizProvider)
        {
            _quizService = quizService;
            _quizProvider = quizProvider;
        }

        // the request to send to the LLM
        // got the response and parsing it
        // save the response to the database

        [HttpPost("new")]
        public async Task<IActionResult> CreateQuiz(CreateQuizDto createQuizDto)
        {
            return _quizProvider.CreateQuiz(createQuizDto);
        }

        // all the quizzes created by the current logged in user
        // or the specific quiz

        [HttpGet("list")]
        public async Task<IActionResult> GetQuizzes(int? userId, int? quizId)
        {
            return _quizProvider.ListQuiz(userId, quizId);
        }

        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetQuizDetail(int id)
        {
            return _quizProvider.GetQuizDetail(id);
        }

        // accept the selected answer for all the Qs and validate them
        // then return the result (stat for correct/wrong, LLM feedback, all Q/A pairs)

        [HttpPost("answer")]
        public async Task<IActionResult> AnswerQuiz(AnswerQuizDto answerQuizDto)
        {
            return _quizProvider.AnswerQuiz(answerQuizDto);
        }
    }
}