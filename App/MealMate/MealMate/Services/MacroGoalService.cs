using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace MealMate.Services
{
    public class MacroGoalService : IMacroGoal
    {


        private readonly HttpClient _httpClient;
        public MacroGoalService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateMacroGoal(MacroGoal newMacroGoal)
        {
            string token = await SecureStorage.GetAsync("auth_token");

            var request = new HttpRequestMessage(HttpMethod.Post, "");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var jsonoptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            request.Content = JsonContent.Create(newMacroGoal, options:jsonoptions);

            var response = await _httpClient.SendAsync(request);




            if (response.IsSuccessStatusCode)
            {
                //MacroGoalResponse responseObj = await response.Content.ReadFromJsonAsync<MacroGoalResponse>();
                return;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"At opdatere userens mål fejlede {response.StatusCode}, {errorContent}");
            }
        }
    }
}
