﻿using MealMate.Services.Interfaces;
using System.Net.Http.Json;
using System.Net.Http;

namespace MealMate.Services;

public class MacroLogService : IMacroLogService
{
    string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI2N2M3MWI0ZjQxYjk4M2M0ZGZjN2NkMjYiLCJpYXQiOjE3NDE3ODkwMTEsImV4cCI6MTc0MTc5MjYxMX0.G2iJfe2byi82pBwse-fuHM9Gcq9hmBa3GyjvInFmHxc";
    MacroLog macroLog = new();
    List<MacroLog> macroLogs = new();
    private readonly HttpClient _httpClient; // HttpClient instance for making HTTP requests

    // Constructor to initialize ApiService with base URL
    public MacroLogService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<MacroLog> CreateMacroLog(MacroLogRequest newMacroLog)
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

    public async Task<List<MacroLog>> GetTodaysMacroLogs()
    {
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
