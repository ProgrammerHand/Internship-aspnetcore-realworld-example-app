using Conduit.Infrastructure.Repository.Interfaces;

namespace Conduit.Features.Article.Application.Commands
{
    public class AddTagsData
    {
        public List<string> names { get; set; }
        public string title { get; set; }
    }

    public class AddTags
    {
        private readonly ITagsRepository _repositoryTags;
        private readonly CreateTags _createTags;
        private readonly IArticleRepository _repositoryArticle;

        public AddTags(ITagsRepository repositoryTags, IArticleRepository repositoryArticle , CreateTags createTags)
        {
            _repositoryTags = repositoryTags;
            _repositoryArticle = repositoryArticle;
            _createTags = createTags;
        }

        public async Task<bool> AddTagsToArticle(List<string> names, string title, int author)
        {
            var tags = await _createTags.CreateTagsList(names);
            var entity = await _repositoryArticle.GetArticleByTitleAndUser(title, author);
            entity.SetTags(tags);
            await _repositoryArticle.UpdateArticle(entity);
            return await _repositoryArticle.Save();
        }
    }
}
