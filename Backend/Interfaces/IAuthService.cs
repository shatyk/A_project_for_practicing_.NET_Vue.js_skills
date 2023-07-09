using Backend.Models.Requests;
using Backend.Models.Responses;

namespace Backend.Interfaces
{
    public interface IAuthService
    {
        Task<AuthenticateResponse> LoginAsync(LoginRequest request, CancellationToken cancellationToken);
        Task<AuthenticateResponse> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken);
        Task<AuthenticateResponse> RefreshAsync(RefreshTokenRequest request, CancellationToken cancellationToken);
        Task LogoutAsync(string username, CancellationToken cancellationToken);
    }
}
