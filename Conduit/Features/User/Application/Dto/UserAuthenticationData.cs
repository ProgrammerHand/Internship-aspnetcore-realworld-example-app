
namespace Conduit.Features.User.Application.Dto
{
    public class UserAuthenticationData
    {
            public string Email { get; init; }
            public string Password { get; init; }
    }

    public record UserAuthenticationDataEnvelop(UserAuthenticationData user);
}
