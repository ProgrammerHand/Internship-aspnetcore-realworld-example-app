using Conduit.Infrastructure;
using Microsoft.EntityFrameworkCore;


namespace Conduit.Features.User.Application.Queries
{
    public class GetCurrent
    {
        private readonly ConduitContext _context;

        public GetCurrent(ConduitContext context)
        {
            _context = context;
        }

        public async Task<Entities.User> GetUserByUsername(string username)
        {
            return await _context.Users.FirstOrDefaultAsync(x => x.Username == username);
        }
    }
}
