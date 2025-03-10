using MealMate.Services.Interfaces;
using System.Net.Http.Json;

namespace MealMate.Services;

public class FoodService : IFoodService
{
    List<Food> foodList = new();
    private readonly HttpClient _httpClient; // HttpClient instance for making HTTP requests

    // Constructor to initialize ApiService with base URL
    public FoodService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Food>> GetAllFoods()
    {
        string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI2N2M3MWI0ZjQxYjk4M2M0ZGZjN2NkMjYiLCJpYXQiOjE3NDE2MjM5MTQsImV4cCI6MTc0MTYyNzUxNH0.hTaRR8v8xB_X_QIKRC3wQVpiF4JoiFoYIK9JF2rMYRg";
        var request = new HttpRequestMessage(HttpMethod.Get, "");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            FoodListResponse responseObj = await response.Content.ReadFromJsonAsync<FoodListResponse>();
            foodList = responseObj.Foods;
        }

        return foodList;
    }

    public Task<Food> GetFoodById(string id)
    {
        throw new NotImplementedException();
    }
}
