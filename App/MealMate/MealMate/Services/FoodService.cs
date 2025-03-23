using MealMate.Services.Interfaces;
using System.Net.Http.Json;

namespace MealMate.Services;

public class FoodService : IFoodService
{
    string token;
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
        token = await SecureStorage.GetAsync("auth_token");
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
        token = await SecureStorage.GetAsync("auth_token");
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
        token = await SecureStorage.GetAsync("auth_token");
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
        token = await SecureStorage.GetAsync("auth_token");
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

    public async Task<Food> UpdateFood(Food food, string id)
    {
        token = await SecureStorage.GetAsync("auth_token");
        var request = new HttpRequestMessage(HttpMethod.Patch, $"{id}");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        request.Content = JsonContent.Create(food);

        var response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var updatedFood = await response.Content.ReadFromJsonAsync<Food>();
            return updatedFood;
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Update failed: {response.StatusCode}, {errorContent}");
        }
    }

    public async Task<Food> DeleteFood(string id)
    {
        token = await SecureStorage.GetAsync("auth_token");
        var request = new HttpRequestMessage(HttpMethod.Delete, $"{id}");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            var deletedFood = await response.Content.ReadFromJsonAsync<Food>();
            return deletedFood;
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Delete failed: {response.StatusCode}, {errorContent}");
        }
    }

}
