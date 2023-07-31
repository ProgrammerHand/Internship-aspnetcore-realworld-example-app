using Conduit.Entities;
using Conduit.Features.Article.Application.Dto;

namespace Conduit.Infrastructure.Repository.Interfaces
{
    public interface IArticleRepository
    {
        Task<bool> CreateArticleInDatabase(Article article);
        Task<bool> UpdateArticle(Article article);
        Task<User> GetUserById(int id);
        Task<Article> GetArticleBySlug(string slug);
        Task<Article> GetArticleByTitleAndUser(string title, int userId);
        Task<Article> GetArticleBySlugAndUser(string slug, int userId);
        Task<IEnumerable<Comment>> GetArticleCommentsBySlug(string slug);
        Task<ICollection<ArticleFeed>> GetArticlesByUser(int userId, int limit, int offset);
        Task<Article> GetArticleByTitle(string title);
        Task<bool> IsExistUsersArticle(int id);
        Task<bool> IsExistArticle(string slug);

        Task<bool> Save();
    }
}
