using Conduit.Entities;
using Conduit.Features.User.Application.Dto;
using Conduit.Infrastructure;
using Conduit.Infrastructure.Security.Interface;
using Microsoft.EntityFrameworkCore;


namespace Conduit.Features.User.Application.Queries
{
    public class GetCurrent
    {
        private readonly ConduitContext _context;
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

        public GetCurrent(ConduitContext context, IJWTtoken jwtService)
        {
            _context = context;
            _jvtService = jwtService;
        }

        public async Task<AunthenticatedUserData> GetAunthenticatedUserById(int id)
        {
             return await _context.Users.AsNoTracking().Select(x => new AunthenticatedUserData { id = x.Id, email = x.Email, role =x.Role, username = x.Username, bio = x.Bio, image = x.Image }).FirstOrDefaultAsync(x => x.id == id);
        }

        public async Task<AunthenticatedUser> GetCurrentUser(int id) 
        {
            var aunthUserData = await GetAunthenticatedUserById(id);
            return new AunthenticatedUser { email = aunthUserData.email, token = _jvtService.CreateToken(aunthUserData.username, aunthUserData.role, aunthUserData.id.ToString()), username = aunthUserData.username, bio = aunthUserData.bio, image = aunthUserData.image};
        }
    }
}
