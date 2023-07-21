using Conduit.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Features.User.Application.Queries
{
    public class GetAll
    {
        private readonly ConduitContext _context;

        public GetAll(ConduitContext context)
        {
            _context = context;
        }

        public async Task<List<Entities.User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
