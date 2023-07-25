using Conduit.Features.Article.Application.Dto;
using Conduit.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Features.Article.Infrastructure.Repository
{
    public class ArticleRepository
    {
        private readonly ConduitContext _context;
        public ArticleRepository(ConduitContext context)
        {
            _context = context;
        }


        public async Task<bool> CreateArticleDatabase(Entities.Article article)
        {
            await _context.Articles.AddAsync(article);
            return await Save();
        }
        public async Task<Entities.User> GetUserById(int id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<ICollection<ArticleFeed>> GetArticlesByUser(int userId, int limit, int offset)
        {
            return await _context.Articles.AsNoTracking()
                .Where(x => x.AuthorId == userId)
                .Select(x => new ArticleFeed { slug = x.Slug, title = x.Title, description = x.Description, body = x.Body, author = x.Author, createdAt = x.CreatedAt, updatedAt = x.UpdatedAt })
                .Skip(offset)
                .Take(limit == 0 ? limit : 10)
                .ToListAsync();
        }

        private async Task<bool> IsExisUsersArticle(int id)
        {
            return await _context.Articles.AnyAsync(x => x.AuthorId == id);
        }
        public async Task<bool> IsExisArticle(string title)
        {
            return await _context.Articles.AnyAsync(x => x.Title == title);
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }
    }
}
