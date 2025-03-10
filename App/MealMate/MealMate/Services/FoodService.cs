using MealMate.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace MealMate.Services
{
    class FoodService
    {

        private readonly HttpClient _httpClient; // HttpClient instance for making HTTP requests

        // Constructor to initialize ApiService with base URL
        public FoodService()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://192.168.1.5:5000/api/"); // Replace with your own IP address
        }

        // Method to fetch list of foods from API asynchronously
        public async Task<Food> getFoodList(string token)
        {

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            // Send GET request to API endpoint for foods
            var response = await _httpClient.GetAsync("foods");

            var contentresponse = await response.Content.ReadAsStringAsync();

            var foods = JsonConvert.DeserializeObject<Food>(contentresponse);


            try
            {
                return foods; // Return list of foods obtained from API
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP request failed: {ex.Message}");
                return null; // Return null in case of HTTP request failure
            }
        }
    }
}
