using MealMate.Services.Interfaces;
using System.Net.Http.Json;

namespace MealMate.Services;

public class LoginService : ILoginService
{
    private readonly HttpClient _httpClient;
    private string _authToken;

    public LoginService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<User> Login(string email, string password)
    {
        var loginRequest = new { email = email, password = password };
        var response = await _httpClient.PostAsJsonAsync("/api/auth/login", loginRequest);

        if (response.IsSuccessStatusCode)
        {
            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            _authToken = loginResponse.Token;
            return loginResponse.User;
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Login failed: {response.StatusCode}, {errorContent}");
        }
    }
}

public class LoginResponse
{
    public string Token { get; set; }
    public User User { get; set; }
}