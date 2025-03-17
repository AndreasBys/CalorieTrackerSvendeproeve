using MealMate.Services.Interfaces;
using Microsoft.Maui.ApplicationModel.Communication;
using System.Net.Http.Json;

namespace MealMate.Services;

public class LoginService : ILoginService
{
    private readonly HttpClient _httpClient;
    private const string _authToken = "auth_token";
    private const string _userId = "user_id";

    public LoginService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<User> Login(string email, string password)
    {
        var loginRequest = new { Email = email, Password = password };
        var response = await _httpClient.PostAsJsonAsync("/api/auth/login", loginRequest);

        if (response.IsSuccessStatusCode)
        {
            var loginResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();
            await SecureStorage.SetAsync(_authToken, loginResponse.Token);
            await SecureStorage.SetAsync(_userId, loginResponse.User._id);
            return loginResponse.User;
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Login failed: {response.StatusCode}, {errorContent}");
        }
    }

    public async Task<User> Registrer(User user)
    {

        var registrerRequest = user;
        var response = await _httpClient.PostAsJsonAsync("/api/auth/register", registrerRequest);

        if (response.IsSuccessStatusCode)
        {
            var registrerResponse = await response.Content.ReadFromJsonAsync<LoginResponse>();

            await Login(user.email, user.password); // Login user after registration 

            return registrerResponse.User;
        }
        else
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            throw new Exception($"Register failed: {response.StatusCode}, {errorContent}");
        }

    }
}

public class LoginResponse
{
    public string Token { get; set; }
    public User User { get; set; }
}