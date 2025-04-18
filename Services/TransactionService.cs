using Newtonsoft.Json;
using SmartFinFront.Models;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace SmartFinFront.Services
{
    public class TransactionService(ApiService apiService)
    {
        private readonly string _baseUrl = "https://localhost:7015/api/Transaction";
        private readonly ApiService _apiService = apiService;

        public async Task<Transaction> GetUserTransactionById(int transactionId, string userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}/{transactionId}?userId={userId}");
            var response = await _apiService.SendRequestAsync(request);
            response.EnsureSuccessStatusCode();
            return JsonSerializer.Deserialize<Transaction>(await response.Content.ReadAsStringAsync())
                ?? throw new Exception("Transaction is null");
        }

        public async Task<IEnumerable<Transaction>> GetUserTransactions(int userId)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{_baseUrl}?userId={userId}");
            var response = await _apiService.SendRequestAsync(request);
            response.EnsureSuccessStatusCode();
            Console.WriteLine(await response.Content.ReadAsStringAsync());
            return JsonConvert.DeserializeObject<IEnumerable<Transaction>>(await response.Content.ReadAsStringAsync())
                ?? throw new Exception("Transactions list is null");
        }

        public async Task<string> CreateUserTransaction(Transaction createTransaction)
        {
            var json = JsonSerializer.Serialize(new
            {
                sum = createTransaction.sum,
                Date = createTransaction.Date,
                Name = createTransaction.Name,
                UserId = createTransaction.UserId,
                CategoryId = createTransaction.CategoryId

            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Post, _baseUrl) { Content = content };
            var response = await _apiService.SendRequestAsync(request);
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> UpdateUserTransaction(Transaction updateTransaction)
        {
            var json = JsonSerializer.Serialize(new
            {
                sum = updateTransaction.sum,
                Date = updateTransaction.Date,
                Name = updateTransaction.Name,
                CategoryId = updateTransaction.CategoryId
            });
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var request = new HttpRequestMessage(HttpMethod.Put, $"{_baseUrl}/{updateTransaction.id}?userId={updateTransaction.UserId}") { Content = content };
            var response = await _apiService.SendRequestAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<string> DeleteTransaction(Transaction transaction)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, $"{_baseUrl}/{transaction.id}?userId={transaction.UserId}");
            var response = await _apiService.SendRequestAsync(request);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadAsStringAsync();
        }

        public async Task<IEnumerable<Transaction>> ImportBankStatement(int userId, string bankName, MultipartFormDataContent content)
        {
            var response = await _apiService.SendRequestAsync(
                new HttpRequestMessage(HttpMethod.Post, $"{_baseUrl}/import?userId={userId}&bankName={bankName}")
                {
                    Content = content
                });

            response.EnsureSuccessStatusCode();

            var result = JsonSerializer.Deserialize<dynamic>(
                await response.Content.ReadAsStringAsync());

            return JsonSerializer.Deserialize<IEnumerable<Transaction>>(
                result.GetProperty("transactions").ToString());
        }
    }
}
