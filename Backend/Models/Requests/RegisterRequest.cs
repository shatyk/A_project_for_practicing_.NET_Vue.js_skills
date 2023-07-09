namespace Backend.Models.Requests
{
    public class RegisterRequest
    {
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
        public string InviteSecretCode { get; set; } = null!;
    }
}
