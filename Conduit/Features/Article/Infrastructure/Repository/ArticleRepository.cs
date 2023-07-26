﻿using Conduit.Features.Article.Application.Dto;
using Conduit.Features.Article.Application.Interfaces;
using Conduit.Infrastructure;
using Microsoft.EntityFrameworkCore;
using static Ply.TplPrimitives;

namespace Conduit.Features.Article.Infrastructure.Repository
{
    public class ArticleRepository: IArticleRepository
    {
        private readonly ConduitContext _context;
        public ArticleRepository(ConduitContext context)
        {
            _context = context;
        }


        public async Task CreateArticleDatabase(Entities.Article article)
        {
            await _context.Articles.AddAsync(article);
        }

        public async Task<Entities.User> GetUserById(int id)
        {
            return await _context.Users.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Entities.Article> GetArticleByTitle(string title) 
        {
            return await _context.Articles.AsNoTracking().FirstOrDefaultAsync(x => x.Title == title);
        }

        public async Task<ICollection<ArticleFeed>> GetArticlesByUser(int userId, int limit, int offset)
        {
            return await _context.Articles.AsNoTracking()
                .Where(x => x.AuthorId == userId)
                .Select(x => new ArticleFeed { slug = x.Slug, title = x.Title, description = x.Description, body = x.Body, author = x.Author, tagList = x.TagList, createdAt = x.CreatedAt, updatedAt = x.UpdatedAt })
                .Skip(offset)
                .Take(limit == 0 ? limit : 10)
                .ToListAsync();
        }

        public async Task<bool> IsExisUsersArticle(int id)
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
