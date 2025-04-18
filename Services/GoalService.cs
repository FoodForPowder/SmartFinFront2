using MudBlazor;
using Newtonsoft.Json;
using SmartFinFront.Models;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SmartFinFront.Services
{
    public class GoalService
    {
        private readonly string _baseUrl = "https://localhost:7015/api/Goal";
        private readonly ApiService _apiService;

        public GoalService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<Goal> GetUsersGoalById(int goalId, int userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/{goalId}?userId={userId}");
            var response = await _apiService.SendRequestAsync(request);
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<Goal>(await response.Content.ReadAsStringAsync())
                ?? throw new Exception("Goal is null");
        }

        public async Task<IEnumerable<Goal>> GetUsersGoals(int userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}?userId={userId}");
            var response = await _apiService.SendRequestAsync(request);
            response.EnsureSuccessStatusCode();
            return JsonConvert.DeserializeObject<IEnumerable<Goal>>(await response.Content.ReadAsStringAsync())
                ?? throw new Exception("Goals list is null");
        }

        public async Task<string> CreateUserGoal(Goal goal)
        {
            var json = JsonSerializer.Serialize(new
            {
                dateOfStart = goal.dateOfStart,
                dateOfEnd = goal.dateOfEnd,
                payment = goal.payment,
                name = goal.name,
                description = goal.description,
                plannedSum = goal.plannedSum,
                UserId = goal.UserId.FirstOrDefault()

            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, _baseUrl) { Content = content };
            var response = await _apiService.SendRequestAsync(request);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> UpdateUserGoal(Goal goal)
        {
            var json = JsonSerializer.Serialize(new
            {
                dateOfStart = goal.dateOfStart,
                dateOfEnd = goal.dateOfEnd,
                payment = goal.payment,
                name = goal.name,
                description = goal.description,
                plannedSum = goal.plannedSum,
                currentSum = goal.currentSum,
                status = goal.status,
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_baseUrl}/{goal.id}?userId={goal.UserId.FirstOrDefault()}") { Content = content };
            var response = await _apiService.SendRequestAsync(request);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteGoal(Goal goal)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/{goal.id}?userId={goal.UserId}");
            var response = await _apiService.SendRequestAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
        public async Task<GoalResponse> RecalculateGoal(Goal goal)
        {
            var json = JsonSerializer.Serialize(goal);
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/recalculate") { Content = new StringContent(json, Encoding.UTF8, "application/json") };

            var response = await _apiService.SendRequestAsync(request);

            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<GoalResponse>(await response.Content.ReadAsStringAsync())
                ?? throw new Exception("Goal is null");

        }

        public async Task ContributeToGoalAsync(int goalId, decimal amount, int userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_baseUrl}/{goalId}/contribute?userId={userId}&amount={amount}");
            var response = await _apiService.SendRequestAsync(request);
            var a = await response.Content.ReadAsStringAsync();
            response.EnsureSuccessStatusCode();
        }

        public async Task<string> GenerateInviteLink(int goalId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/{goalId}/invite");
            var response = await _apiService.SendRequestAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task JoinGoal(int goalId, int userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/join/{goalId}?userId={userId}");
            var response =  await _apiService.SendRequestAsync(request);
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception(await response.Content.ReadAsStringAsync());
            }
        }


        public class GoalResponse
        {
            public string message { get; set; }
            public Goal goal { get; set; }
        }
    }
}
