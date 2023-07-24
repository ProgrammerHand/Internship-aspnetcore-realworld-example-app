using Conduit.Entities;
using Conduit.Features.Article.Application.Dto;
using Conduit.Infrastructure;
using Duende.IdentityServer.Models;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Features.Article.Application.Queries
{
    public class Feed
    {

        private readonly ConduitContext _context;

        public record feedOptions 
        {
            
        }

        public Feed(ConduitContext context)
        {
            _context = context;
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

        public async Task<ArticleFeedEnvelop> FeedArticles(int userId, int limit, int offset)
        {

            if (await IsExisUsersArticle(userId))
            {
                var list = await GetArticlesByUser(userId, limit, offset);
                return new ArticleFeedEnvelop (list, list.Count ) ;
            }
            else
                throw new ArgumentException("No articles found");

        }
    }
}
