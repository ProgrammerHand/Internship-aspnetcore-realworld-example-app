
namespace Conduit.Infrastructure.Security.Interface
{
    public interface IHashingService
    {
        (byte[], byte[]) HashPassword(string password);
        bool VerifyPassword(string inputPassword, UserAunthCredentials dbPassword);
    }

    public record UserAunthCredentials
    {
        public string email { get; init; }
        public byte[] passwordHash { get; init; }
        public byte[] passwordSalt { get; init; }
    }
}
