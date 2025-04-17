using System.Net.Http.Json;
using System.Text.Json;
using SmartFinFront.Models;

namespace SmartFinFront.Services
{
    public class CategoryService
    {
        private readonly ApiService _apiService;
        private readonly string _baseUrl = "https://localhost:7015/api/Category";

        public CategoryService(ApiService apiService)
        {
            _apiService = apiService;
        }

        public async Task<IEnumerable<Category>> GetUserCategories()
        {
            var request = new HttpRequestMessage(HttpMethod.Get, _baseUrl);
            var response = await _apiService.SendRequestAsync(request);
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<IEnumerable<Category>>(await response.Content.ReadAsStringAsync())
                ?? throw new Exception("Categories list is null");
        }

        public async Task<Category> GetCategory(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/{id}");
            var response = await _apiService.SendRequestAsync(request);
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<Category>(await response.Content.ReadAsStringAsync())
                ?? throw new Exception("Category is null");
        }

        public async Task<string> CreateCategory(Category category)
        {
            var json = JsonSerializer.Serialize(new
            {
                name = category.name,
            });
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, _baseUrl) { Content = content };
            var response = await _apiService.SendRequestAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> UpdateCategory(int id, Category category)
        {
            var json = JsonSerializer.Serialize(new
            {
                name = category.name,
            });
            var content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_baseUrl}/{id}") { Content = content };
            var response = await _apiService.SendRequestAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteCategory(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/{id}");
            var response = await _apiService.SendRequestAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }
    }
}