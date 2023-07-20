namespace Conduit.Infrastructure.security
{
    public interface IJWTtoken
    {
        string CreateToken(string username, string role, string id);
    }
}
