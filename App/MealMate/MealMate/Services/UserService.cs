using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace MealMate.Services;

public class UserService : IUserService
{
    private readonly HttpClient _httpClient;
    //private const string _authToken = "auth_token";

    public UserService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<User> GetUserByIdAsync(string id)
    {
        var response = await _httpClient.GetAsync($"/{id}");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadFromJsonAsync<User>();
    }

    public async Task<User> UpdateUserAsync(User user, string id)
    {
        var response = await _httpClient.PutAsJsonAsync($"/{id}", user);
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

    public async Task DeleteUserAsync(string id)
    {
        var response = await _httpClient.DeleteAsync($"/{id}");
        response.EnsureSuccessStatusCode();
    }
}
