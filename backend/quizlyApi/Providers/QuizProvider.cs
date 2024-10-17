using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using quizlyApi.Data;
using quizlyApi.DTOs;
using quizlyApi.Models;
using quizlyApi.Utilities;

namespace quizlyApi.Providers;

public class QuizProvider
{
    private readonly string _now = DateTime.Now.ToString("yyMMdd_HHmmss");
    private readonly QuizlyDbContext _dbContext = new();

    public QuizProvider(QuizlyDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<LLMUtilResponse> UtilCalLLM(string title, Language language, string context, Difficulty difficulty)
    {
        // mock call
        // #TODO this one differs between the providers

        // var (content, rawResponse) = await LLM.GenerateQuestionsAsync(context);

        var diffcultyString = string.Empty;
        switch (difficulty)
        {
            case Difficulty.MostEasy:
            case Difficulty.Easy:
            case Difficulty.Medium:
            case Difficulty.Hard:
            case Difficulty.MostHard:
                diffcultyString = difficulty.ToString();
                break;

            default:
                diffcultyString = "Medium";
                break;
        }

        var languageString = string.Empty;
        switch (language)
        {
            case Language.ZH:
                languageString = "Chinese";
                break;

            case Language.EN:
                languageString = "English";
                break;

            default:
                languageString = "English";
                break;
        }

        var quizTitle = title;
        var quizLanguage = languageString;
        var quizDifficulty = diffcultyString;
        var quizContext = context;

        var eventualPrompt = $@"
            INPUT: {quizTitle}, language: {quizLanguage}, difficulty: {quizDifficulty}, 5 questions
            CONTEXT: {quizContext}

            Given the input above and some of the context given (INPUT has higher priority than CONTEXT),
            please generate given amount of single-choice questions in a JSON format with the following structure:

            The field 'answer_dissection' shall
            - convince enough people to choose the correct answer (why, when, how etc.).
            - start from first principle, then go the technical details
            - use simple and clear English, avoid technical jargon

            {{
              ""metadata"": {{
                ""total_q"": N
              }},
              ""content"": [
                {{
                  ""id"": 1,
                  ""problem"": """",
                  ""answer"": """",
                  ""answer_dissection"": """",
                  ""options"": [
                    """",
                    """",
                    """",
                    """"
                  ]
                }},
                {{
                  ""id"": 2,
                  ""problem"": """",
                  ""answer"": """",
                  ""answer_dissection"": """",
                  ""options"": [
                    """",
                    """",
                    """",
                    """"
                  ]
                }},
                {{
                  ""id"": 3,
                  ""problem"": """",
                  ""answer"": """",
                  ""answer_dissection"": """",
                  ""options"": [
                    """",
                    """",
                    """",
                    """"
                  ]
                }}
              ]
            }}

            Please fill in the ""problem"", ""answer"", ""answer_dissection"", and ""options"" fields with appropriate values based on the input provided.
            The field 'options' is a list of answers that only contain one correct answer.
            The field 'answer' must be one of the options.
            The field 'answer' is the correct answer to the problem.

            You must follow the instructions I gave you exactly.
        ";


        var (content, rawResponse) = await LLM.GenerateQuestionsAsync(eventualPrompt);

        if (string.IsNullOrEmpty(content) || string.IsNullOrEmpty(rawResponse))
        {
            return new LLMUtilResponse()
            {
                _success = false,
                _message = "LLM response is empty",
                RawContent = rawResponse,
                PostProcessedContent = content
            };
        }

        return new LLMUtilResponse()
        {
            _success = true,
            RawContent = rawResponse,
            PostProcessedContent = content
        };
    }

    public IActionResult CreateQuiz(CreateQuizDto createQuizDto)
    {
        using (var db = new QuizlyDbContext())
        {
            #region param-validation

            if (createQuizDto == null)
            {
                return new JsonResult(new
                {
                    message = "No payload provided",
                    success = false
                });
            }

            if (string.IsNullOrEmpty(createQuizDto.title))
            {
                return new JsonResult(new
                {
                    message = "Title is required",
                    success = false
                });
            }
            if (string.IsNullOrEmpty(createQuizDto.context))
            {
                return new JsonResult(new
                {
                    message = "Context is required",
                    success = false
                });
            }

            if (createQuizDto.difficulty < Difficulty.MostEasy || createQuizDto.difficulty > Difficulty.MostHard)
            {
                return new JsonResult(new
                {
                    message = "Difficulty ranged from 1 to 5 (MostEasy to MostHard)",
                    success = false
                });
            }

            createQuizDto.language ??= Language.EN;
            createQuizDto.difficulty ??= Difficulty.Medium;

            #endregion

            #region call-llm

            var llmResponse = UtilCalLLM(
                createQuizDto.title,
                createQuizDto.language.Value,
                createQuizDto.context,
                createQuizDto.difficulty.Value
            );

            if (llmResponse.Result._success == false)
            {
                return new JsonResult(new
                {
                    message = llmResponse.Result._message,
                    success = false
                });
            }

            #endregion


            #region parse-n-save-to-db

            var newQuizConfig = new QuizConfig()
            {
                Title = createQuizDto.title,
                Context = createQuizDto.context,
                Difficulty = Difficulty.Medium
            };

            db.QuizConfigs.Add(newQuizConfig);
            db.SaveChanges();

            var newQuizContent = new QuizContent()
            {
                QuizId = newQuizConfig.Id,
                UserId = createQuizDto.userId,

                RawContent = llmResponse.Result.RawContent,
                PostProcessedContent = llmResponse.Result.PostProcessedContent,
            };


            db.QuizContents.Add(newQuizContent);
            db.SaveChanges();

            #endregion

            return new JsonResult(new
            {
                message = "Quiz created successfully",
                success = true
            });
        }
    }

    public IActionResult ListQuiz(int? userId, int? quizId)
    {
        using (var db = new QuizlyDbContext())
        {
            if (userId == null && quizId == null)
            {
                return new JsonResult(new
                {
                    message = "Need to provide at least one of userId or quizId",
                    success = false
                });
            }

            var quizList = new List<QuizDto>();

            if (userId != null)
            {
                quizList = db.QuizContents.Where(x => x.UserId == userId).Select(x => new QuizDto()
                {
                    Id = x.QuizId,
                    Title = x.QuizConfig.Title,
                    Difficulty = x.QuizConfig.Difficulty,
                    RawContent = x.RawContent ?? string.Empty,
                    PostProcessedContent = x.PostProcessedContent
                }).ToList();
            }

            if (quizId != null)
            {
                quizList = db.QuizContents.Where(x => x.QuizId == quizId).Select(x => new QuizDto()
                {
                    Id = x.QuizId,
                    Title = x.QuizConfig.Title,
                    Difficulty = x.QuizConfig.Difficulty,
                    RawContent = x.RawContent ?? string.Empty,
                    PostProcessedContent = x.PostProcessedContent
                }).ToList();
            }

            if (quizList.Count == 0)
            {
                return new JsonResult(new
                {
                    message = "No quiz found",
                    success = false
                });
            }

            return new JsonResult(new
            {
                data = quizList,
                message = "Quiz list retrieved successfully",
                success = true,
            });
        }
    }

    public IActionResult GetQuizDetail(int id)
    {
        using (var db = new QuizlyDbContext())
        {
            var quiz = db.QuizContents.Where(x => x.QuizId == id).FirstOrDefault();
            if (quiz == null)
            {
                return new JsonResult(new
                {
                    message = "Quiz not found",
                    success = false
                });
            }

            if (string.IsNullOrEmpty(quiz.PostProcessedContent))
            {
                return new JsonResult(new
                {
                    message = "Quiz not generated or processed yet",
                    success = false
                });
            }

            var parsedContent = new LLMQuizOptionResponse();
            try
            {
                parsedContent = JsonConvert.DeserializeObject<LLMQuizOptionResponse>(quiz.PostProcessedContent);
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    message = ex.Message,
                    success = false
                });
            }

            if (parsedContent == null)
            {
                return new JsonResult(new
                {
                    message = "Quiz content is empty",
                    success = false
                });
            }

            return new JsonResult(new
            {
                data = parsedContent,
                message = "Quiz detail retrieved successfully",
                success = true,
            });
        }
    }

    public IActionResult AnswerQuiz(AnswerQuizDto answerQuizDto)
    {
        using (var db = new QuizlyDbContext())
        {
            if (answerQuizDto.UserId == null || answerQuizDto.QuizId == null)
            {
                return new JsonResult(new
                {
                    message = "Need to provide at least one of userId or quizId",
                    success = false
                });
            }

            if (answerQuizDto.UserId < 1)
            {
                return new JsonResult(new
                {
                    message = "UserId must be greater than 0",
                    success = false
                });
            }
            if (answerQuizDto.QuizId < 1)
            {
                return new JsonResult(new
                {
                    message = "QuizId must be greater than 0",
                    success = false
                });
            }

            var quiz = db.QuizContents.Where(x => x.QuizId == answerQuizDto.QuizId).FirstOrDefault();
            if (quiz == null)
            {
                return new JsonResult(new
                {
                    message = "Quiz not found",
                    success = false
                });
            }

            if (string.IsNullOrEmpty(quiz.PostProcessedContent))
            {
                return new JsonResult(new
                {
                    message = "Quiz not generated or processed yet",
                    success = false
                });
            }

            var parsedContent = new LLMQuizOptionResponse();
            try
            {
                parsedContent = JsonConvert.DeserializeObject<LLMQuizOptionResponse>(quiz.PostProcessedContent);
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    message = ex.Message,
                    success = false
                });
            }

            if (parsedContent == null)
            {
                return new JsonResult(new
                {
                    message = "Quiz content is empty",
                    success = false
                });
            }

            var questionTotal = parsedContent.metadata.total_q;

            int correctCount = 0;
            foreach (var answer in parsedContent.content)
            {
                if (answer.answer == answerQuizDto.Answers.FirstOrDefault(c => c.Answer == answer.answer)?.Answer)
                {
                    correctCount++;
                }
            }

            var summary = string.Empty;
            foreach (var answer in parsedContent.content)
            {
                summary += $"{answer.problem}: {answer.answer_dissection}\n";
            }

            return new JsonResult(new
                {
                    data = new
                    {
                        questionTotal = questionTotal,
                        correctCount = correctCount,
                        summary = summary,
                    },
                    message = "Quiz answer retrieved successfully",
                    success = true,
                });
        }
    }
}
