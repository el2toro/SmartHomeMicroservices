namespace SmartHome.Models.Auth
{
    public class AuthResponse
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public DateTime Expiration { get; set; }
        public string Token { get; set; }
    }
}
