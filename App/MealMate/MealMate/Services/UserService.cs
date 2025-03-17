using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MealMate.Services
{
    public class UserService : IUserService
    {
        private readonly HttpClient _httpClient;
        public UserService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }


        public async Task<User> UpdateUser(User user)
        {
            string token = await SecureStorage.GetAsync("auth_token");
            string id = await SecureStorage.GetAsync("user_id");

            var request = new HttpRequestMessage(HttpMethod.Patch, id);
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

            var jsonoptions = new JsonSerializerOptions
            {
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull
            };

            request.Content = JsonContent.Create(user, options: jsonoptions);

            var response = await _httpClient.SendAsync(request);

            


            if (response.IsSuccessStatusCode)
            {
                UserResponse responseObj = await response.Content.ReadFromJsonAsync<UserResponse>();

                return responseObj.user;
            }
            else
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                throw new Exception($"At opdatere useren fejlede {response.StatusCode}, {errorContent}");
            }


        }
    }
}
