using Conduit.Features.User.Application.Interface;
using Conduit.Features.User.Domain;
using Conduit.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Features.User.Application
{
    public class Registration
    {
        private readonly ConduitContext _context;
        private readonly IHashingService _hashingService;

        public record UserRegisterCredentials
        {
            public string username { get; init; }
            public string email { get; init; }
            public byte[] passwordHash { get; init; }
        }

        public Registration(ConduitContext context, IHashingService hashingService)
        {
            _context = context;
            _hashingService = hashingService;
        }

        private async Task<bool> IsExisUser(string email, string username)
        {
            return await _context.Users.AnyAsync(x => x.Email == email && x.Username == username);
        }

        private async Task<bool> CreateUser(Domain.User newUser)
        {
            await _context.Users.AddAsync(newUser);
            return await Save();
        }
        private async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<bool> Register(UserRegistration data)
        {

            if (!await IsExisUser(data.email, data.username))
            {
                var hash = _hashingService.HashPassword(data.password);
                return await CreateUser(Domain.User.CreateUser(data.email, data.username, hash.Item1, hash.Item2));
            }
            else
                throw new ArgumentException("User with such username or email alredy exists");

        }
    }
}
