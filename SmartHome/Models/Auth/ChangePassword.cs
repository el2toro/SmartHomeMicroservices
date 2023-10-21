namespace SmartHome.Models.Auth
{
    public class RessetPassword
    {
        public string Username { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
    }
}
