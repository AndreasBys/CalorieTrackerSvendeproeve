﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MealMate.Services
{
    public class RetterService : IRetterService
    {

        string token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VySWQiOiI2N2NiMjFiNzg4ZjU1ZmZlMTliMjM5YTYiLCJpYXQiOjE3NDE5OTM0MTEsImV4cCI6MTc0MTk5NzAxMX0.9O1_l7BFH0u93TfVQAdCHqLrVqVpVXp61O5tZE1_7tg";

        private readonly HttpClient _httpClient;

        List<Retter> retterList = new();

        public RetterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public Task<Retter> CreateRet(Retter newFood)
        {
            throw new NotImplementedException();
        }

        public async Task<List<Retter>> GetAllRetter()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, "");
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                RetterListResponse responseObj = await response.Content.ReadFromJsonAsync<RetterListResponse>();
                retterList = responseObj.dishes;
            }

            return retterList;
        }


        public async Task<List<Retter>> SearchRetter(string searchTerm)
        {
            Retter retterObj = new Retter();

            var request = new HttpRequestMessage(HttpMethod.Get, "search?searchTerm=" + searchTerm);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var response = await _httpClient.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                RetterListResponse responseObj = await response.Content.ReadFromJsonAsync<RetterListResponse>();
                retterList = responseObj.dishes;
            }

            return retterList;
        }
    }
}
