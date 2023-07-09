using Backend.Interfaces.Jwt;
using Backend.Models;
using Database.Models;
using System.Security.Claims;

namespace Backend.Services.Jwt
{
    public class AccessTokenService : IAccessTokenService
    {
        private readonly ITokenGenerator _tokenGenerator;
        private readonly JwtSettings _jwtSettings;

        public AccessTokenService(ITokenGenerator tokenGenerator, JwtSettings jwtSettings) =>
            (_tokenGenerator, _jwtSettings) = (tokenGenerator, jwtSettings);

        public string Generate(User user)
        {
            List<Claim> claims = new()
            {
                new Claim("id", user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };
            return _tokenGenerator.Generate(_jwtSettings.AccessTokenSecret, _jwtSettings.Issuer, _jwtSettings.Audience,
                _jwtSettings.AccessTokenExpirationMinutes, claims);
        }
    }
}
