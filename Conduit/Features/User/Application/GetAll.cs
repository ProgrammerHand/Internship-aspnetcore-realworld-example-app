using Conduit.Features.User.Domain;
using Conduit.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Features.User.Application
{
    public class GetAll
    {
        private readonly ConduitContext _context;

        public GetAll(ConduitContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }
    }
}
