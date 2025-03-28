﻿using MealMate.Services.Interfaces;
using System.Net.Http.Json;
using System.Net.Http;

namespace MealMate.Services;

public class MacroLogService : IMacroLogService
{
    MacroLog macroLog = new();
    List<MacroLog> macroLogs = new();
    private readonly HttpClient _httpClient; // HttpClient instance for making HTTP requests

    // Constructor to initialize ApiService with base URL
    public MacroLogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<MacroLog> CreateMacroLog(dynamic newMacroLog)
    {
        if (!(newMacroLog is MacroLogRequest log1) && !(newMacroLog is MacroLogDishRequest log2))
        {
            throw new Exception("Invalid type");
        }

        string token = await SecureStorage.GetAsync("auth_token");
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

    public async Task<MacroLog> CreateMacroLog(MacroLogDishRequest newMacroLog)
    {
        string token = await SecureStorage.GetAsync("auth_token");
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

    public async Task<List<MacroLog>> GetTodaysMacroLogs()
    {
        string token = await SecureStorage.GetAsync("auth_token");
        var request = new HttpRequestMessage(HttpMethod.Get, "");
        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

        var response = await _httpClient.SendAsync(request);

        if (response.IsSuccessStatusCode)
        {
            MacroLogListResponse responseObj = await response.Content.ReadFromJsonAsync<MacroLogListResponse>();
            macroLogs = responseObj.macroLogs;
        }

        return macroLogs;
    }

}
