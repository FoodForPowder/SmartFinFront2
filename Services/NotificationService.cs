using Newtonsoft.Json;
using SmartFinFront.Models;

namespace SmartFinFront.Services
{
    public class NotificationService
    {
        private readonly ApiService _apiService;
        private readonly string _baseUrl = "https://localhost:7015/api/Notification";

        public NotificationService(ApiService apiService)
        {
            _apiService = apiService;
        }

        /// <summary>
        /// Получает список уведомлений пользователя
        /// </summary>
        public async Task<List<Notification>> GetUserNotifications(int userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/user/{userId}");
            var response = await _apiService.SendRequestAsync(request);
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<List<Notification>>(content) ?? new List<Notification>();
        }
    }
}
