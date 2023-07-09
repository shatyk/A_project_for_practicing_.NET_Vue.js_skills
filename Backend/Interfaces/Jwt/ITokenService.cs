using Database.Models;

namespace Backend.Interfaces.Jwt
{
    public interface ITokenService
    {
        string Generate(User user);
    }
}
