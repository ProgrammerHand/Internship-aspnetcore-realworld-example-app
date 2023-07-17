using Conduit.Features.User.Application.Interface;
using Conduit.Features.User.Domain;
using Conduit.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Features.User.Application
{
    public class Authentication
    {
        private readonly ConduitContext _context;
        private readonly IHashingService _hashingService;

        public record UserAunthCredentials
        {
            public string email { get; init; }
            public byte[] passwordHash { get; init; }
            public byte[] passwordSalt { get; init; }
        }

        public Authentication(ConduitContext context, IHashingService hashingService)
        {
            _context = context;
            _hashingService = hashingService;
        }

        private async Task<UserAunthCredentials> GetPasswordHash(string email)
        {
            return await _context.Users.Select(x => new UserAunthCredentials { email = x.Email, passwordHash = x.PasswordHash, passwordSalt = x.PasswordSalt }).FirstOrDefaultAsync(x => x.email == email);
        }

        private async Task<AunthenticatedUser> GetAunthUser(string email)
        {
            return await _context.Users.Select(x => new AunthenticatedUser { email = x.Email, token = null, username = x.Username, bio = x.Bio, image = x.Image }).FirstOrDefaultAsync(x => x.email == email);
        }

        public async Task<AunthenticatedUser> Authenticate(UserAunthenication data)
        {
            var credentials = await GetPasswordHash(data.Email);

            if (_hashingService.VerifyPassword(credentials.passwordHash, data.Password, credentials.passwordSalt))
                return await GetAunthUser(data.Email);
            else
                throw new ArgumentException("Wrong password or email");

        }
    }
}
