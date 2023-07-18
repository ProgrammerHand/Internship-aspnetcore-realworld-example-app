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

        private async Task<Domain.User> GetPasswordHash(string email)
        {
            //var temp = 
            //var temp1 = await temp;
            //return await _context.Users.Select(x => new UserAunthCredentials { email = x.Email, passwordHash = x.PasswordHash, passwordSalt = x.PasswordSalt }).FirstOrDefaultAsync(x => x.email == email);
            return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        private async Task<AunthenticatedUser> GetAunthUser(string email)
        {
            return await _context.Users.AsNoTracking().Select(x => new AunthenticatedUser { email = x.Email, token = null, role =x.Role, username = x.Username, bio = x.Bio, image = x.Image }).FirstOrDefaultAsync(x => x.email == email);
        }

        public async Task<UserEnvelop> Authenticate(UserAunthenication data)
        {
            var credentials = await GetPasswordHash(data.Email);

            if (_hashingService.VerifyPassword(credentials.PasswordHash, data.Password, credentials.PasswordSalt))
            { 
                var temp = await GetAunthUser(data.Email);
                temp.token = _jvtService.CreateToken(temp.username, temp.role);
                return new UserEnvelop(temp);
                }
            else
                throw new ArgumentException("Wrong password or email");

        }
    }
}
