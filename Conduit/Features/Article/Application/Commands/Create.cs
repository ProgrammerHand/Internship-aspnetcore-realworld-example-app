using Conduit.Features.User.Application.Dto;
using Conduit.Infrastructure.Security.Interface;
using Conduit.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Conduit.Features.Article.Application.Dto;

namespace Conduit.Features.Article.Application.Commands
{
    public class Create
    {

        private readonly ConduitContext _context;

        public Create(ConduitContext context)
        {
            _context = context;
        }

        private async Task<bool> IsExisArticle(string title)
        {
            return await _context.Articles.AnyAsync(x => x.Title == title );
        }

        private async Task<bool> CreateArticleDatabase(ArticleCreation data, int AuthorId)
        {
            await _context.Articles.AddAsync(Entities.Article.CreateArticle(data.title, data.description, data.body, data.tagList, await GetUserById(AuthorId)));
            return await Save();
        }
        private async Task<bool> Save()
        {
            var saved = _context.SaveChangesAsync();
            return await saved > 0 ? true : false;
        }

        public async Task<Entities.User> GetUserById(int id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<bool> CreateArticle(ArticleCreation data, int authorId)
        {

            if (!await IsExisArticle(data.title))
            {
                return await CreateArticleDatabase(data, authorId);
            }
            else
                throw new ArgumentException("Article with such title or email alredy exists");

        }
    }
}
