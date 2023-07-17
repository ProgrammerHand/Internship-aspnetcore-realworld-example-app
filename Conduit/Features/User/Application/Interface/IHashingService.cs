namespace Conduit.Features.User.Application.Interface
{
    public interface IHashingService
    {
        (byte[], byte[]) HashPassword(string password);
        bool VerifyPassword(byte[] passwordHash, string inputPassword, byte[] salt);
    }
}
