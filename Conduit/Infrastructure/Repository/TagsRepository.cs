using Conduit.Entities;
using Conduit.Infrastructure;
using Conduit.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Conduit.Infrastructure.Repository
{
    public class TagsRepository : ITagsRepository
    {
        private readonly ConduitContext _context;
        public TagsRepository(ConduitContext context)
        {
            _context = context;
        }

        public async Task<Tag> GetTag(string name)
        {
            return await _context.Tags.AsNoTracking().FirstOrDefaultAsync(x => x.TagName == name);
        }

        public async Task<List<Tag>> GetTagsByName(IEnumerable<string> names)
        {
            return await _context.Tags.Include(t => t.Articles).Where(t => names.Contains(t.TagName)).ToListAsync();
        }

        public async Task CreateTag(Tag tag)
        {
            await _context.Tags.AddAsync(tag);
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }
    }
}
