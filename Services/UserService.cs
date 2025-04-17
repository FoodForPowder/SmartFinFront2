using Newtonsoft.Json;
using SmartFinFront.Models;
using System.Text;
using System.Text.Json;

namespace SmartFinFront.Services
{
    public class UserService
    {
        private readonly ApiService _apiService;
        private readonly string _baseUrl = "https://localhost:7015" + "/api/User";

        public UserService(ApiService apiService)
        {
            _apiService = apiService;
            
        }

        public async Task<List<User>> GetUsers()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _baseUrl);
            var response = await _apiService.SendRequestAsync(request);
            response.EnsureSuccessStatusCode();
            return System.Text.Json.JsonSerializer.Deserialize<List<User>>(await response.Content.ReadAsStringAsync()) ?? throw new Exception("Users list is null");
        }

        public async Task<User> GetUser(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/{id}");
            var response = await _apiService.SendRequestAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<User>(await response.Content.ReadAsStringAsync()) ?? throw new Exception("User is null");
        }

        public async Task<string> UpdateUser(string id, User user)
        {
            var json = JsonConvert.SerializeObject(new
            {
                Name = user.Name,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_baseUrl}/{id}") { Content = content };
            var response = await _apiService.SendRequestAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> ChangePassword(ChangePassRequest changePassRequest)
        {
            var json = JsonConvert.SerializeObject(changePassRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_baseUrl}/changepass") { Content = content };
            var response = await _apiService.SendRequestAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task DeleteUser(string id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/{id}");
            var response = await _apiService.SendRequestAsync(request);
            response.EnsureSuccessStatusCode();
        }

        public async Task<string> SetExpenseLimit(int id, decimal? limit)
        {
            var json = JsonConvert.SerializeObject(limit);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_baseUrl}/{id}/expense-limit") { Content = content };
            var response = await _apiService.SendRequestAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> SetMonthlyIncome(int id, decimal income)
        {
            var json = JsonConvert.SerializeObject(income);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_baseUrl}/{id}/monthly-income") { Content = content };
            var response = await _apiService.SendRequestAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}
