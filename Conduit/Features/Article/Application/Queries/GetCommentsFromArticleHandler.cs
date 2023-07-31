using Conduit.Entities;
using Conduit.Features.Article.Application.Dto;
using Conduit.Infrastructure.Repository.Interfaces;

namespace Conduit.Features.Article.Application.Queries
{
    public record GetCommentsFromArticleCommand(List<Comment> comments);
    public class GetCommentsFromArticleHandler
    {
        private readonly IArticleRepository _repository;

        public GetCommentsFromArticleHandler(IArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<GetCommentsFromArticleCommand> GetComments(string slug)
        {
            if (await _repository.IsExistArticle(slug) is false)
                throw new ArgumentException("No articles found");
            var test = (await _repository.GetArticleCommentsBySlug(slug)).ToList();
            return new GetCommentsFromArticleCommand(test);
        }
    }
}
