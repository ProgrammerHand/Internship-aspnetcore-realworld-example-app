using Conduit.Infrastructure;

namespace Conduit.Features.User.Application
{
    public class GetCurrent
    {
        private readonly ConduitContext _context;

        public GetCurrent(ConduitContext context)
        {
            _context = context;
        }
    }
}
