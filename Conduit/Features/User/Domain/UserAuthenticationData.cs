
namespace Conduit.Features.User.Domain
{
    public class UserAuthenticationData
    {
            public string Email { get; init; }
            public string Password { get; init; }
    }

    public record UserAuthenticationDataEnvelop(UserAuthenticationData authenticationData);
}
