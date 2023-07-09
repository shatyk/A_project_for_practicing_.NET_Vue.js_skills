namespace Backend.Models.Responses
{
    public class AuthenticateResponse
    {
        public string AccessToken { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public double AccessLifetime { get; set; }
        public double RefreshLifetime { get; set; }
    }
}
