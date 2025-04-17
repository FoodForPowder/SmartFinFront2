using System.Net.Http.Headers;

namespace SmartFinFront.Services
{
    public class ApiService(HttpClient httpClient, AuthService authService)
    {
        private readonly AuthService _authService = authService;
        private readonly HttpClient _httpClient = httpClient;

        public async Task<HttpResponseMessage> SendRequestAsync(HttpRequestMessage message, CancellationToken cancellationToken = default, TimeSpan? timeout = null)
        {           
            message.Headers.Authorization = new AuthenticationHeaderValue ($"Bearer",await _authService.GetToken());
            if (timeout.HasValue) httpClient.Timeout = timeout.Value;
            return await httpClient.SendAsync(message, cancellationToken);
        }

    }
}
