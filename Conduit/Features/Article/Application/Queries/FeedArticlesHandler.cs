using Conduit.Features.Article.Application.Dto;
using Conduit.Infrastructure.Repository.Interfaces;

namespace Conduit.Features.Article.Application.Queries
{
    public class FeedArticlesHandler
    {

        private readonly IArticleRepository _repository;

        public FeedArticlesHandler(IArticleRepository repository)
        {
            _repository = repository;
        }

        
        public async Task<ArticleFeedEnvelop> FeedArticles(int userId, int limit, int offset)
        {

            if (await _repository.IsExisUsersArticle(userId))
            {
                var list = await _repository.GetArticlesByUser(userId, limit, offset);
                return new ArticleFeedEnvelop (list, list.Count ) ;
            }
            else
                throw new ArgumentException("No articles found");

        }
    }
}
