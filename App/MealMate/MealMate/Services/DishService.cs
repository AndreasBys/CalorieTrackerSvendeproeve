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
            string token = await SecureStorage.GetAsync("auth_token");


            var request = new HttpRequestMessage(HttpMethod.Get, "");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                DishListResponse responseObj = await response.Content.ReadFromJsonAsync<DishListResponse>();
                retterList = responseObj.dishes;
                return retterList;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"At hente retterne fejlede {response.StatusCode}, {errorContent}");
            }

        }


        public async Task<List<Dish>> SearchRetter(string searchTerm)
        {
            Dish retterObj = new Dish();
            string token = await SecureStorage.GetAsync("auth_token");



            var request = new HttpRequestMessage(HttpMethod.Get, "search?searchTerm=" + searchTerm);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                DishListResponse responseObj = await response.Content.ReadFromJsonAsync<DishListResponse>();
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
