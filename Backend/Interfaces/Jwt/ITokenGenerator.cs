using System.Security.Claims;

namespace Backend.Interfaces.Jwt
{
    public interface ITokenGenerator
    {
        string Generate(string secretKey, string issuer, string audience, double expires,
            IEnumerable<Claim>? claims = null);
    }
}
