namespace Conduit.Infrastructure.Security.Interface
{ 
    public interface IJWTtoken
    {
        string CreateToken(string username, string role, string id);
    }
}
