namespace Conduit.Features.User.Domain
{
    public class AunthenticatedUser
    {
        public string email { get; init; }
        public string token { get; set; } = null;
        public string username { get; init; }
        public string bio { get; init; } = null;
        public string image { get; init; } = null;

    }
}
