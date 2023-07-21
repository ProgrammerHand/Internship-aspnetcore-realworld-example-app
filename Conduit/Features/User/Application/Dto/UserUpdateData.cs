namespace Conduit.Features.User.Application.Dto
{
    public class UserUpdateData
    {
        public string email { get; set; }
        public string password { get; set; }
        public string username { get; init; }
        public string bio { get; init; }
        public string image { get; init; }
    }

    public record UserUpdateDataEnvelop(UserUpdateData updateData);
}
