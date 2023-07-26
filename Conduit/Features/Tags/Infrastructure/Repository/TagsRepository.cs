using Conduit.Entities;
using Conduit.Features.Tags.Application.Interfaces;
using Conduit.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Features.Tags.Infrastructure.Repository
{
    public class TagsRepository: ITagsRepository
    {
        private readonly ConduitContext _context;
        public TagsRepository(ConduitContext context)
        {
            _context = context;
        }

        public async Task<Entities.Tags> GetTag(string name) 
        {
            return await _context.Tags.AsNoTracking().FirstOrDefaultAsync(x => x.TagName == name);
        }

        public async Task<Entities.Tags> GetArticleTag(string name, int articleId)
        {
            return await _context.Tags.AsNoTracking().FirstOrDefaultAsync(x => x.TagName == name && x.ArticleId == articleId);
        }

        public async Task<List<string>> GetUniqueTags()
        {
            return await _context.Tags.AsNoTracking().Select(t => t.TagName).Distinct().ToListAsync();
        }
        
        public async Task CreateTag(Entities.Tags tag) 
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
