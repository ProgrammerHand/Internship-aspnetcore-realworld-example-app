namespace Conduit.Features.User.Application.Dto
{
    public class UserRegistrationData
    {
        public string username { get; init; }
        public string email { get; init; }
        public string password { get; init; }
    }
    public record UserRegistrationDataEnvelop(UserRegistrationData registrationData);
}
