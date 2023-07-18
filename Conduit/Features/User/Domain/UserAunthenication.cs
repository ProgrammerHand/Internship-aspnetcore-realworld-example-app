
namespace Conduit.Features.User.Domain
{
    public class UserAunthenication
    {
            public string Email { get; init; }
            public string Password { get; init; }
    }

    public class RootUserAunthenication 
    {
        public UserAunthenication user { get; init; }
    }
}
