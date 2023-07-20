using Conduit.Features.User.Application.Interface;
using Conduit.Features.User.Domain;
using Conduit.Infrastructure;
using Conduit.Infrastructure.security;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Features.User.Application
{
    public class Authentication
    {
        private readonly ConduitContext _context;
        private readonly IHashingService _hashingService;
        private readonly IJWTtoken _jvtService;

        public record UserAunthCredentials
        {
            public string email { get; init; }
            public byte[] passwordHash { get; init; }
            public byte[] passwordSalt { get; init; }
        }

        public Authentication(ConduitContext context, IHashingService hashingService, IJWTtoken jwtService)
        {
            _context = context;
            _hashingService = hashingService;
            _jvtService = jwtService;
        }

        private async Task<UserAunthCredentials> GetPasswordHash(string email)
        {
            return await _context.Users.Select(x => new UserAunthCredentials { email = x.Email, passwordHash = x.PasswordHash, passwordSalt = x.PasswordSalt }).FirstOrDefaultAsync(x => x.email == email);
            //return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        private async Task<AunthenticatedUser> GetAunthUser(string email)
        {
            return await _context.Users.AsNoTracking().Select(x => new AunthenticatedUser { email = x.Email, token = null, role =x.Role, username = x.Username, bio = x.Bio, image = x.Image }).FirstOrDefaultAsync(x => x.email == email);
        }

        public async Task<AunthenticateUserEnvelop> Authenticate(UserAuthenticationData data)
        {
            if (_hashingService.VerifyPassword(data.Password, await GetPasswordHash(data.Email)))
            {
                var aunthUser = await GetAunthUser(data.Email);
                aunthUser.token = _jvtService.CreateToken(aunthUser.username, aunthUser.role);
                return new AunthenticateUserEnvelop(aunthUser);
                }
            else
                throw new ArgumentException("Wrong password or email");

        }
    }
}
