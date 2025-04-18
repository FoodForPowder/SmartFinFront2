using SmartFinFront.Models;

namespace SmartFinFront.Services
{
    public class AuthService
    {
        private string _url = "https://localhost:7015/api/Auth";

        private static HttpClient _httpClient = new HttpClient();

        public UserAuthInfo _userAuthInfo { get; set; }

        public async Task<UserAuthInfo> Login(string email, string password)
        {
            UserAuthInfo userAuthInfo = null;

            var response = await _httpClient.PostAsJsonAsync(_url + "/login", new
            {
                email = email,
                password = password
            });

            if (response.IsSuccessStatusCode)
            {
                userAuthInfo = await response.Content.ReadFromJsonAsync<UserAuthInfo>();
                _userAuthInfo = userAuthInfo;
            }

            return userAuthInfo;
        }
        public string GetToken()
        {

            return _userAuthInfo.AccessToken ?? throw new Exception("_userAuthInfo is null");

        }
    }
}
