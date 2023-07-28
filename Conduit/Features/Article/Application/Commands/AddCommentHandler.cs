using Conduit.Infrastructure.Repository.Interfaces;

namespace Conduit.Features.Article.Application.Commands
{
    public class AddCommentHandler
    {
        private readonly CreateTagsHandler _createTags;
        private readonly IArticleRepository _repositoryArticle;

        public AddCommentHandler(IArticleRepository repositoryArticle, CreateTagsHandler createTags)
        {
            _repositoryArticle = repositoryArticle;
        }

        public async Task<bool> AddCommentoArticle(string body, string slug, int author)
        {
            var entity = await _repositoryArticle.GetArticleByTitleAndUser(slug, author);
            //entity.AddTags(tags);
            await _repositoryArticle.UpdateArticle(entity);
            return await _repositoryArticle.Save();
        }
    }
}
