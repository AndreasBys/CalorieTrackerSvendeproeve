using MealMate.Services.Interfaces;
using System.Net.Http.Json;

namespace MealMate.Services;

public class FoodService : IFoodService
{
    string token = "";
    List<Food> foodList = new();
    Food food = new();
    private readonly HttpClient _httpClient; // HttpClient instance for making HTTP requests

    // Constructor to initialize ApiService with base URL
    public FoodService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<List<Food>> GetAllFoods()
    {
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

    public async Task<Food> GetFoodByBarcode(string barcode)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, barcode);
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            food = await response.Content.ReadFromJsonAsync<Food>();
        }

        return food;
    }
}
