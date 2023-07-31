using Conduit.Infrastructure.Repository.Interfaces;

namespace Conduit.Features.Article.Application.Commands
{
    public class AddTagsHandler
    {
        private readonly TagsCreationHandler _createTags;
        private readonly IArticleRepository _repositoryArticle;

        public AddTagsHandler(IArticleRepository repositoryArticle , TagsCreationHandler createTags)
        {
            _repositoryArticle = repositoryArticle;
            _createTags = createTags;
        }

        public async Task<bool> AddTagsToArticle(List<string> names, string title, int author)
        {
            var tags = await _createTags.CreateTagsList(names);
            var entity = await _repositoryArticle.GetArticleByTitleAndUser(title, author);
            entity.AddTags(tags);
            await _repositoryArticle.UpdateArticle(entity);
            return await _repositoryArticle.Save();
        }
    }
}
