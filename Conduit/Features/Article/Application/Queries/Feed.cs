using Conduit.Entities;
using Conduit.Features.Article.Application.Dto;
using Conduit.Features.Article.Application.Interfaces;
using Conduit.Infrastructure;
using Duende.IdentityServer.Models;
using Microsoft.EntityFrameworkCore;

namespace Conduit.Features.Article.Application.Queries
{
    public class Feed
    {

        private readonly IArticleRepository _repository;

        public Feed(IArticleRepository repository)
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
