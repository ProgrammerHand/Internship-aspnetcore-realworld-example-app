using Conduit.Infrastructure.Repository.Interfaces;

namespace Conduit.Features.Article.Application.Commands
{
    public class DeleteTagsFromArticleHandler
    {

        private readonly ITagsRepository _repositoryTags;
        private readonly TagsCreationHandler _createTags;
        private readonly IArticleRepository _repositoryArticle;

        public DeleteTagsFromArticleHandler(ITagsRepository repositoryTags, IArticleRepository repositoryArticle, TagsCreationHandler createTags)
        {
            _repositoryTags = repositoryTags;
            _repositoryArticle = repositoryArticle;
            _createTags = createTags;
        }

        public async Task<bool> DeleteTagsFromArticle(List<string> names, string title, int author)
        {
            var entity = await _repositoryArticle.GetArticleByTitleAndUser(title, author);
            entity.DeleteTags(names);
            await _repositoryArticle.UpdateArticle(entity);
            return await _repositoryArticle.Save();
        }
    }
}
