using Conduit.Features.User.Application.Dto;
using Conduit.Infrastructure;
using Conduit.Infrastructure.Security.Interface;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Features.User.Application.Commands
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
        public async Task<AunthenticatedUser> UpdateUserDatabase(UserUpdateData data, int id)
        {
            if (!await IsExistUser(id, data.email, data.username))
            {
                var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
                var newHashsalt = _hashingService.HashPassword(data.password);
                user.UpdateUser(data, newHashsalt.Item1, newHashsalt.Item2);
                _context.Users.Update(user);
                await Save();
                return new AunthenticatedUser { email = user.Email, token = _jvtService.CreateToken(user.Username, user.Role, user.Id.ToString()), role = user.Role, username = user.Username, bio = user.Bio, image = user.Image };
            }
            else
                throw new ArgumentException("User with such email or username alredy exist");
        }

        private async Task<bool> IsExistUser(int id, string email, string username)
        {
            return await _context.Users.AnyAsync(x =>( x.Email == email || x.Username == username) && x.Id != id);
        }
        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<AunthenticateUserEnvelop> UpdateUser(UserUpdateData data, int id )
        {
            return new AunthenticateUserEnvelop(await UpdateUserDatabase(data, id));
        }
    }
}
