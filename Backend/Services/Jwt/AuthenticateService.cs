using Backend.Interfaces.Jwt;
using Backend.Models;
using Backend.Models.Responses;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services.Jwt
{
    public class AuthenticateService : IAuthenticateService
    {
        private readonly IAccessTokenService _accessTokenService;
        private readonly IRefreshTokenService _refreshTokenService;
        private readonly AppDbContext _appDbContext;
        private readonly JwtSettings _jwtSettings;

        public AuthenticateService(IAccessTokenService accessTokenService, IRefreshTokenService refreshTokenService,
            AppDbContext appDbContext, JwtSettings jwtSettings)
        {
            _accessTokenService = accessTokenService;
            _refreshTokenService = refreshTokenService;
            _appDbContext = appDbContext;
            _jwtSettings = jwtSettings;
        }

        public async Task<AuthenticateResponse> AuthenticateAsync(User user, CancellationToken cancellationToken)
        {
            string? refreshToken = _refreshTokenService.Generate(user);
            await _appDbContext.RefreshTokens.AddAsync(new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken
            }, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);
            return new AuthenticateResponse
            {
                AccessToken = _accessTokenService.Generate(user),
                RefreshToken = refreshToken,
                AccessLifetime = _jwtSettings.AccessTokenExpirationMinutes,
                RefreshLifetime = _jwtSettings.RefreshTokenExpirationMinutes
            };
        }
    }
}
