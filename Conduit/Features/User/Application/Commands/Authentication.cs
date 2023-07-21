using Conduit.Features.User.Application.Dto;
using Conduit.Infrastructure;
using Conduit.Infrastructure.Security.Interface;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Features.User.Application.Commands
{
    public class Authentication
    {
        private readonly ConduitContext _context;
        private readonly IHashingService _hashingService;
        private readonly IJWTtoken _jvtService;

        public record AunthenticatedUserData
        {
            public int id { get; init; }
            public string email { get; init; }
            public string role { get; init; }
            public string username { get; init; }
            public string bio { get; init; } = null;
            public string image { get; init; } = null;
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

        private async Task<AunthenticatedUserData> GetAunthUser(string email)
        {
            return await _context.Users.AsNoTracking().Select(x => new AunthenticatedUserData { id = x.Id, email = x.Email, role =x.Role, username = x.Username, bio = x.Bio, image = x.Image }).FirstOrDefaultAsync(x => x.email == email);
        }

        public async Task<AunthenticatedUserEnvelop> Authenticate(UserAuthenticationData data)
        {
            if (_hashingService.VerifyPassword(data.Password, await GetPasswordHash(data.Email)))
            {
                var aunthUserData =  await GetAunthUser(data.Email);
                return new AunthenticatedUserEnvelop(new AunthenticatedUser
                {
                    email = aunthUserData.email,
                    username = aunthUserData.username,
                    token =_jvtService.CreateToken(aunthUserData.username, aunthUserData.role, aunthUserData.id.ToString()),
                    bio = aunthUserData.bio,
                    image = aunthUserData.image
                });
            }
            else
                throw new ArgumentException("Wrong password or email");

        }
    }
}
