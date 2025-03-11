using MealMate.Services.Interfaces;
using System.Net.Http.Json;
using System.Net.Http;

namespace MealMate.Services;

class MacroLogService : IMacroLogService
{
    string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI2N2M3MWI0ZjQxYjk4M2M0ZGZjN2NkMjYiLCJpYXQiOjE3NDE3MTIwODgsImV4cCI6MTc0MTcxNTY4OH0.KvdnCuEdjmXyFCarY1IV7pLFkf4k0jsAQTFjn8vyt-w";
    MacroLog macroLog = new();
    private readonly HttpClient _httpClient; // HttpClient instance for making HTTP requests

    // Constructor to initialize ApiService with base URL
    public MacroLogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<MacroLog> CreateMacroLog(MacroLog newMacroLog)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        request.Content = JsonContent.Create(newMacroLog);

        var response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            MacroLogResponse responseObj = await response.Content.ReadFromJsonAsync<MacroLogResponse>();
            macroLog = responseObj.macroLog;
        }

        return macroLog;
    }
}
