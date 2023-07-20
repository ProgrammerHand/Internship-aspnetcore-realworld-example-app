namespace Conduit.Features.User.Domain
{
    public class UserUpdateData
    {
        public string email { get; private set; }
        public string password { get; private set; }
        public string username { get; init; }
        public string bio { get; init; }
        public string image { get; init; }
    }

    public record UserUpdateDataEnvelop(UserUpdateData updateData);
}
