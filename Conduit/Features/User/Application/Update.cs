using Conduit.Features.User.Application.Interface;
using Conduit.Features.User.Domain;
using Conduit.Infrastructure;
using Conduit.Infrastructure.security;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Features.User.Application
{
    public class Update
    {
        private readonly ConduitContext _context;
        private readonly IHashingService _hashingService;
        private readonly IJWTtoken _jvtService;
        public Update(ConduitContext context, IHashingService hashingService, IJWTtoken jwtService)
        {
            _context = context;
            _hashingService = hashingService;
            _jvtService = jwtService;
        }
        public async Task<AunthenticatedUser> UpdateUserDatabase(UserUpdateData data)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Username == data.username);
            user.UpdateUser(data, _hashingService.HashPassword(data.password).Item1);
            _context.Users.Update(user);
            await Save();
            return new AunthenticatedUser { email = user.Email, token = _jvtService.CreateToken(user.Username, user.Role, user.Id.ToString()), role = user.Role, username = user.Username, bio = user.Bio, image = user.Image };
        }
        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<AunthenticateUserEnvelop> UpdateUser(UserUpdateData data)
        {
            return new AunthenticateUserEnvelop(await UpdateUserDatabase(data));
        }
    }
}
