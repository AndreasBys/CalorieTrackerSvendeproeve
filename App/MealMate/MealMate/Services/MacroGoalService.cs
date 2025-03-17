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

        public async Task<MacroGoal> CreateMacroGoal(MacroGoal newMacroGoal)
        {
            string token = await SecureStorage.GetAsync("auth_token");
            string id = await SecureStorage.GetAsync("user_id");

            newMacroGoal.id = id;

            var request = new HttpRequestMessage(HttpMethod.Post, "");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);


            request.Content = JsonContent.Create(newMacroGoal);

            var response = await _httpClient.SendAsync(request);




            if (response.IsSuccessStatusCode)
            {
                MacroGoalResponse responseObj = await response.Content.ReadFromJsonAsync<MacroGoalResponse>();

                return responseObj.macroGoal;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"At opdatere userens mål fejlede {response.StatusCode}, {errorContent}");
            }
        }
    }
}
