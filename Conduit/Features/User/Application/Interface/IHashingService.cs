using static Conduit.Features.User.Application.Authentication;

namespace Conduit.Features.User.Application.Interface
{
    public interface IHashingService
    {
        (byte[], byte[]) HashPassword(string password);
        bool VerifyPassword(string inputPassword, UserAunthCredentials dbPassword);
    }
}
