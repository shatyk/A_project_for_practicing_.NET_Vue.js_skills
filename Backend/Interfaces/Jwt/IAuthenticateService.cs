using Backend.Models.Responses;
using Database.Models;

namespace Backend.Interfaces.Jwt
{
    public interface IAuthenticateService
    {
        Task<AuthenticateResponse> AuthenticateAsync(User user, CancellationToken cancellationToken);
    }
}
