namespace Conduit.Features.User.Domain
{
    public class UserRegistration
    {
        public string username { get; init; }
        public string email { get; init; }
        public string password { get; init; }
    }
    public class RootUserRegistration
    {
        public UserRegistration user { get; init; }
    }
}
