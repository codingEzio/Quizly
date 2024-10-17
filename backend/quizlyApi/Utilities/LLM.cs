using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace quizlyApi.Utilities
{
    // a class holding all constants
    public static class Constants
    {
        public const string PromptTemplate = @"
            INPUT: {quizTitle}, language: {quizLanguage}, difficulty: {quizDifficulty}, 5 questions
            CONTEXT: {quizContext}

            Given the input above and some of the context given (INPUT has higher priority than CONTEXT),
            please generate given amount of single-choice questions in a JSON format with the following structure:

            The field 'answer_dissection' shall
            - convince enough people to choose the correct answer (why, when, how etc.).
            - start from first principle, then go the technical details
            - use simple and clear English, avoid technical jargon

            {
              ""metadata"": {
                ""total_q"": N
              },
              ""content"": [
                {
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
                },
                {
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
                },
                {
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
                }
              ]
            }

            Please fill in the ""problem"", ""answer"", ""answer_dissection"", and ""options"" fields with appropriate values based on the input provided.
            The field 'options' is a list of answers that only contain one correct answer.
            The field 'answer' must be one of the options.
            The field 'answer' is the correct answer to the problem.

            You must follow the instructions I gave you exactly.
        ";
    }

    public static class LLM
    {
        private static readonly HttpClient client = new HttpClient();

        // Global Variables
        private static readonly string? ApiUrl = Environment.GetEnvironmentVariable("QUIZLY_LLM_API_URI");
        private static readonly string? ApiKey = Environment.GetEnvironmentVariable("QUIZLY_LLM_API_TOKEN");
        private static readonly string? ModelName = Environment.GetEnvironmentVariable("QUIZLY_LLM_API_MODEL");

        private const string UserRole = "user";

        public static async Task<(string content, string rawResponse)> GenerateQuestionsAsync(string prompt)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", ApiKey);

            var requestData = new
            {
                model = ModelName,
                messages = new[]
                {
                    new { role = UserRole, content = prompt }
                }
            };

            var json = JsonConvert.SerializeObject(requestData);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            try
            {
                HttpResponseMessage response = await client.PostAsync(ApiUrl, content);
                response.EnsureSuccessStatusCode();

                var rawResponse = await response.Content.ReadAsStringAsync();
                dynamic result = JsonConvert.DeserializeObject(rawResponse);

                string generatedContent = result.choices[0].message.content;

                // remove starting '```json' and ending '```' using Replace method
                generatedContent = generatedContent.Replace("```json", "").Replace("```", "");
                generatedContent = generatedContent.TrimStart().TrimEnd();

                return (generatedContent, rawResponse);
            }
            catch (HttpRequestException e)
            {
                // Handle HTTP request errors
                Console.WriteLine($"Request error: {e.Message}");
                return (null, null);
            }
            catch (Exception e)
            {
                // Handle other possible errors
                Console.WriteLine($"An error occurred: {e.Message}");
                return (null, null);
            }
        }
    }
}
