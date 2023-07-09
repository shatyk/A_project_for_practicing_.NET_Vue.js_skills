using Backend.Interfaces;
using Backend.Interfaces.Jwt;
using Backend.Models;
using Backend.Models.Requests;
using Backend.Models.Responses;
using Database;
using Database.Models;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;

namespace Backend.Services
{
    public class AuthService : IAuthService
    {
        private readonly AppDbContext _appDbContext;
        private readonly IAuthenticateService _authenticateService;
        private readonly IRefreshTokenValidator _refreshTokenValidator;
        private readonly RegisterInviteSecret _registerInviteSecret;

        public AuthService(AppDbContext appDbContext, IAuthenticateService authenticateService,
            IRefreshTokenValidator refreshTokenValidator, RegisterInviteSecret registerInviteSecret)
        {
            _appDbContext = appDbContext;
            _authenticateService = authenticateService;
            _refreshTokenValidator = refreshTokenValidator;
            _registerInviteSecret = registerInviteSecret;
        }

        public async Task<AuthenticateResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken)
        {
            User? user = await _appDbContext.Users.FirstOrDefaultAsync(u => u.Username == request.Username, cancellationToken);
            if (user is null)
            {
                throw new HttpRequestException("Wrong credentials, dude.", null, HttpStatusCode.Unauthorized);
            }

            if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            {
                throw new HttpRequestException("Wrong credentials, dude.", null, HttpStatusCode.Unauthorized);
            }

            return await _authenticateService.AuthenticateAsync(user, cancellationToken);
        }

        public async Task LogoutAsync(string username, CancellationToken cancellationToken)
        {
            User user = await _appDbContext.Users.FirstAsync(u => u.Username == username, cancellationToken);
            IEnumerable<RefreshToken> refreshTokens = _appDbContext.RefreshTokens.AsNoTracking().Where(x => x.UserId == user.Id);
            _appDbContext.RefreshTokens.RemoveRange(refreshTokens);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<AuthenticateResponse> RefreshAsync(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            if (!_refreshTokenValidator.Validate(request.Token))
            {
                throw new HttpRequestException("Token expired:( Be faster next time.", null, HttpStatusCode.Unauthorized);
            }

            RefreshToken? refreshToken =
                await _appDbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == request.Token, cancellationToken);

            if (refreshToken is null)
            {
                throw new HttpRequestException("Token expired:( Be faster next time.", null, HttpStatusCode.Unauthorized);
            }

            _appDbContext.RefreshTokens.Remove(refreshToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            User? user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Id == refreshToken.UserId, cancellationToken);
            if (user is null)
            {
                throw new HttpRequestException("Token expired:( Be faster next time.", null, HttpStatusCode.Unauthorized);
            }

            return await _authenticateService.AuthenticateAsync(user, cancellationToken);
        }

        public async Task<AuthenticateResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken)
        {
            if (request.InviteSecretCode != _registerInviteSecret.RegisterInviteSecretCode)
            {
                throw new BadHttpRequestException("Wrong invite code, go f*ck yourself, no one called you!");
            }

            User? user = await _appDbContext.Users.FirstOrDefaultAsync(x => x.Username == request.Username, cancellationToken);
            if (user is not null)
            {
                throw new BadHttpRequestException("This name is already taken, be more original, come up with another one!");
            }

            user = new User
            {
                Username = request.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(request.Password, 10),
            };

            await _appDbContext.Users.AddAsync(user, cancellationToken);
            await _appDbContext.SaveChangesAsync(cancellationToken);

            return await _authenticateService.AuthenticateAsync(user, cancellationToken);
        }
    }
}