using Conduit.Features.User.Application.Dto;
using Conduit.Infrastructure.Repository.Interfaces;
using Conduit.Infrastructure.Security.Interface;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Infrastructure.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly ConduitContext _context;

        public UserRepository(ConduitContext context)
        {
            _context = context;
        }

        public async Task<UserAunthCredentials> GetPasswordHash(string email)
        {
            return await _context.Users.Select(x => new UserAunthCredentials { email = x.Email, passwordHash = x.PasswordHash, passwordSalt = x.PasswordSalt }).FirstOrDefaultAsync(x => x.email == email);
            //return await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<AunthenticatedUserData> GetAunthUser(string email)
        {
            return await _context.Users.AsNoTracking().Select(x => new AunthenticatedUserData { id = x.Id, email = x.Email, role = x.Role, username = x.Username, bio = x.Bio, image = x.Image }).FirstOrDefaultAsync(x => x.email == email);
        }

        public async Task<AunthenticatedUserData> GetAunthUser(int id)
        {
            return await _context.Users.AsNoTracking().Select(x => new AunthenticatedUserData { id = x.Id, email = x.Email, role = x.Role, username = x.Username, bio = x.Bio, image = x.Image }).FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<Entities.User> GetUser(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<Entities.User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task UpdateUserDatabase(Entities.User user)
        {
            _context.Users.Update(user);
            await Save();
        }

        public async Task<bool> IsExistUser(int id, string email, string username)
        {
            return await _context.Users.AnyAsync(x => (x.Email == email || x.Username == username) && x.Id != id);
        }

        public async Task<bool> IsExistUser(string email, string username)
        {
            return await _context.Users.AnyAsync(x => x.Email == email && x.Username == username);
        }

        public async Task CreateUser(Entities.User newUser)
        {
            await _context.Users.AddAsync(newUser);
        }
        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }
    }
}
