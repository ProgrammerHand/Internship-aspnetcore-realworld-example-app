
namespace Conduit.Features.User.Domain
{
    public class UserAunthenication
    {
            public string Email { get; init; }
            public string Password { get; init; }
    }

    public record UserAunthenicationEnvelop(UserAunthenication user);
}
