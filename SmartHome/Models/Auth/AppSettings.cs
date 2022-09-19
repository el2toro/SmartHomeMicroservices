namespace SmartHome.Models.Auth
{
    public class AppSettings
    {
        // Properties for JWT Token Signature
        public string Site { get; set; }
        public string Audience { get; set; }
        public string ExpireTime { get; set; }
        public string Secret { get; set; }

        // Send Grid
        public string SendGridKey { get; set; }
        public string SendGridUser { get; set; }
        public string DefaultNamespace { get; set; }
        public string Location { get; set; }
    }
}
