using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace quizlyApi.Utilities
{
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
