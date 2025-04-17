namespace SmartFinFront.Models
{
    public class UserAuthInfo
    {
        public int userId { get; set; }
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
