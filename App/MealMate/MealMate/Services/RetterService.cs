using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MealMate.Services
{
    public class RetterService : IRetterService
    {


        private readonly HttpClient _httpClient;

        List<Retter> retterList = new();

        public RetterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task<Retter> CreateRet(Retter newFood)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Retter>> GetAllRetter()
        {
            string token = await SecureStorage.GetAsync("auth_token");

            var request = new HttpRequestMessage(HttpMethod.Get, "");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                RetterListResponse responseObj = await response.Content.ReadFromJsonAsync<RetterListResponse>();
                retterList = responseObj.dishes;
                return retterList;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"At hente retterne fejlede {response.StatusCode}, {errorContent}");
            }

            
        }


        public async Task<List<Retter>> SearchRetter(string searchTerm)
        {
            string token = await SecureStorage.GetAsync("auth_token");


            var request = new HttpRequestMessage(HttpMethod.Get, "search?searchTerm=" + searchTerm);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                RetterListResponse responseObj = await response.Content.ReadFromJsonAsync<RetterListResponse>();
                retterList = responseObj.dishes;
                return retterList;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"At hente retten fejlede {response.StatusCode}, {errorContent}");
            }

            
        }
    }
}
