using MealMate.Services.Interfaces;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Net.Http.Json;

namespace MealMate.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    private string _authToken;
    User user;


    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        //_authToken = SecureStorage.GetAsync("auth_token").Result;
    }

    public async Task<User> GetUserById(string id)
    {
        _authToken = await SecureStorage.GetAsync("auth_token");
        var request = new HttpRequestMessage(HttpMethod.Get, $"{id}");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _authToken);

        var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response Content: {responseContent}");

            UserResponse userResponse = await response.Content.ReadFromJsonAsync<UserResponse>();
            user = userResponse.user;
            return user;
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Update failed: {response.StatusCode}, {errorContent}");
        }
    }

    public async Task<User> UpdateUser(User user, string id)
    {

        _authToken = await SecureStorage.GetAsync("auth_token");
        var request = new HttpRequestMessage(HttpMethod.Patch,($"{id}"));
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", _authToken);
        request.Content = JsonContent.Create(user);

        var response = await _httpClient.SendAsync(request);
        if (response.IsSuccessStatusCode)
        {
            var updatedUser = await response.Content.ReadFromJsonAsync<User>();
            return updatedUser;
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Update failed: {response.StatusCode}, {errorContent}");
        }
    }
}

