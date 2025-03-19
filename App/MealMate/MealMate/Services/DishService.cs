using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MealMate.Services
{
    public class DishService : IDishService
    {

        string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI2N2NiMjFiNzg4ZjU1ZmZlMTliMjM5YTYiLCJpYXQiOjE3NDE3OTQyODksImV4cCI6MTc0MTc5Nzg4OX0.PAykLP_dS7HtA2ek4ZijnK8YLFYyGkpzUN8z8TuGuwU";

        private readonly HttpClient _httpClient;

        List<Dish> retterList = new();

        public DishService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task<Dish> CreateRet(Dish newFood)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Dish>> GetAllRetter()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                RetterListResponse responseObj = await response.Content.ReadFromJsonAsync<RetterListResponse>();
                retterList = responseObj.dishes;
            }

            return retterList;
        }


        public async Task<List<Dish>> SearchRetter(string searchTerm)
        {
            Dish retterObj = new Dish();

            var request = new HttpRequestMessage(HttpMethod.Get, "search?searchTerm=" + searchTerm);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                RetterListResponse responseObj = await response.Content.ReadFromJsonAsync<RetterListResponse>();
                retterList = responseObj.dishes;
            }

            return retterList;
        }
    }
}
