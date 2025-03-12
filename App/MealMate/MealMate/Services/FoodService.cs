using MealMate.Services.Interfaces;
using System.Net.Http.Json;

namespace MealMate.Services;

public class FoodService : IFoodService
{
    List<Food> foodList = new();
    Food food;
    private readonly HttpClient _httpClient; // HttpClient instance for making HTTP requests

    // Constructor to initialize ApiService with base URL
    public FoodService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Food>> GetAllFoods()
    {
        string token = await SecureStorage.GetAsync("auth_token");
        var request = new HttpRequestMessage(HttpMethod.Get, "");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            FoodListResponse responseObj = await response.Content.ReadFromJsonAsync<FoodListResponse>();
            foodList = responseObj.foods;
        }

        return foodList;
    }
    public async Task<List<Food>> SearchFoods(string searchTerm)
    {
        string token = await SecureStorage.GetAsync("auth_token");
        var request = new HttpRequestMessage(HttpMethod.Get, "search?searchTerm=" + searchTerm);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            FoodListResponse responseObj = await response.Content.ReadFromJsonAsync<FoodListResponse>();
            foodList = responseObj.foods;
        }

        return foodList;
    }

    public async Task<Food> GetFoodByBarcode(string barcode)
    {
        string token = await SecureStorage.GetAsync("auth_token");
        var request = new HttpRequestMessage(HttpMethod.Get, barcode);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            FoodResponse responseObj = await response.Content.ReadFromJsonAsync<FoodResponse>();
            food = responseObj.food;
        }

        return food;
    }

    public async Task<Food> CreateFood(FoodRequest newFood)
    {
        string token = await SecureStorage.GetAsync("auth_token");
        var request = new HttpRequestMessage(HttpMethod.Post, "");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        request.Content = JsonContent.Create(newFood);

        var response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            FoodResponse responseObj = await response.Content.ReadFromJsonAsync<FoodResponse>();
            food = responseObj.food;
        }

        return food;
    }
}
