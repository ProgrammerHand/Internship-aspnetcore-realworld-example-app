﻿using Conduit.Features.Article.Application.Dto;

namespace Conduit.Features.Article.Application.Interfaces
{
    public interface IArticleRepository
    {
        Task CreateArticleDatabase(Entities.Article article);

        Task<Entities.User> GetUserById(int id);
        Task<ICollection<ArticleFeed>> GetArticlesByUser(int userId, int limit, int offset);
        Task<Entities.Article> GetArticleByTitle(string title);
        Task<bool> IsExisUsersArticle(int id);

        Task<bool> IsExisArticle(string title);

        Task<bool> Save();
    }
}
