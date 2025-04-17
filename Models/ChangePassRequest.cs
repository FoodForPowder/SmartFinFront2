namespace SmartFinFront.Models
{
    public class ChangePassRequest
    {

        public string userId { get; set; }

        public string oldPassword { get; set; }


        public string newPassword { get; set; }
    }
}
