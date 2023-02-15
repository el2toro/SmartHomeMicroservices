namespace SmartHome.Models.Auth
{
    public class ChangePassword
    {
        public string Username { get; set; }
        public string CurrentPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
