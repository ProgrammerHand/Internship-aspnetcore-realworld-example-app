using Conduit.Entities;
using Conduit.Features.Article.Application.Dto;
using Conduit.Infrastructure;
using Conduit.Infrastructure.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using static Ply.TplPrimitives;
using System.Linq;
using static FuncyDown.Element.Element;

namespace Conduit.Infrastructure.Repository
{
    public class ArticleRepository : IArticleRepository
    {
        private readonly ConduitContext _context;
        public ArticleRepository(ConduitContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateArticleInDatabase(Article article)
        {
            await _context.Articles.AddAsync(article);
            return await Save();
        }

        public async Task<bool> UpdateArticle(Article article)
        {
            _context.Articles.Update(article);
            return await Save();
        }

        public async Task<User> GetUserById(int id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Article> GetArticleByTitle(string title)
        {
            return await _context.Articles.AsNoTracking().FirstOrDefaultAsync(x => x.Title == title);
        }

        public async Task<Article> GetArticleBySlug(string slug)
        {
            return await _context.Articles.FirstOrDefaultAsync(x => x.Slug == slug);
        }

        public async Task<Article> GetArticleBySlugAndUser(string slug, int userId)
        {
            return await _context.Articles.Include(x => x.Comments).FirstOrDefaultAsync(x => x.Slug == slug && x.AuthorId == userId);
        }

        public async Task<Article> GetArticleByTitleAndUser(string title, int userId)
        {
            return await _context.Articles.Include(x => x.Tags).FirstOrDefaultAsync(x => x.Title == title && x.AuthorId == userId);
        }

        public async Task<ICollection<ArticleFeed>> GetArticlesByUser(int userId, int limit, int offset)
        {
            return await _context.Articles.AsNoTracking()
                .Include(x => x.Tags)
                .Where(x => x.Author.Id == userId)
                .Select(x => new ArticleFeed { slug = x.Slug, title = x.Title, description = x.Description, body = x.Body, author = x.Author, tagList = x.Tags.Select(t => t.TagName), createdAt = x.CreatedAt, updatedAt = x.UpdatedAt })
                .Skip(offset)
                .Take(limit == 0 ? limit : 10)
                .ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetArticleCommentsBySlug(string slug)
        {
            return await _context.Articles.AsNoTracking().Where(x => x.Slug == slug).Select(x => x.Comments).FirstOrDefaultAsync();
        }

        public async Task<bool> IsExistUsersArticle(int id)
        {
            return await _context.Articles.AnyAsync(x => x.Author.Id == id);
        }
        public async Task<bool> IsExistArticle(string slug)
        {
            return await _context.Articles.AnyAsync(x => x.Slug == slug);
        }

        public async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }
    }
}
