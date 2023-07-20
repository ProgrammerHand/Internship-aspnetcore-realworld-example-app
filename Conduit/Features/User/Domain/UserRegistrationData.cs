namespace Conduit.Features.User.Domain
{
    public class UserRegistrationData
    {
        public string username { get; init; }
        public string email { get; init; }
        public string password { get; init; }
    }
    public record UserRegistrationDataEnvelop(UserRegistrationData registrationData);
}
