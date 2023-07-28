using Conduit.Features.Article.Application.Dto;

namespace Conduit.Infrastructure.Repository.Interfaces
{
    public interface IArticleRepository
    {
        Task CreateArticleInDatabase(Entities.Article article);
        Task UpdateArticle(Entities.Article article);
        Task<Entities.User> GetUserById(int id);
        Task<Entities.Article> GetArticleByTitleAndUser(string title, int userId);
        Task<Entities.Article> GetArticleBySlugAndUser(string slug, int userId);
        Task<ICollection<ArticleFeed>> GetArticlesByUser(int userId, int limit, int offset);
        Task<Entities.Article> GetArticleByTitle(string title);
        Task<bool> IsExisUsersArticle(int id);

        Task<bool> IsExisArticle(string title);

        Task<bool> Save();
    }
}
