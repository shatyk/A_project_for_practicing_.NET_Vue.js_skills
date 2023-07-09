namespace Backend.Interfaces.Jwt
{
    public interface IRefreshTokenValidator
    {
        bool Validate(string refreshToken);
    }
}
