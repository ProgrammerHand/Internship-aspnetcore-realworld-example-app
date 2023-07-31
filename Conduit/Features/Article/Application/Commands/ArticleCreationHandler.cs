using Conduit.Infrastructure.Repository.Interfaces;
using Conduit.Infrastructure.Security;

namespace Conduit.Features.Article.Application.Commands
{
    public class ArticleCreationCommand
    {
        public string title { get; set; }
        public string description { get; set; }
        public string body { get; set; }
        public List<string>? tagList { get; set; }
    }

    public class ArticleCreationHandler
    {
        private readonly IArticleRepository _repository;
        private readonly TagsCreationHandler _createTags;

        public ArticleCreationHandler(IArticleRepository repository, TagsCreationHandler createTagsServ)
        {
            _repository = repository;
            _createTags = createTagsServ;
        }

        public async Task<bool> CreateArticle(ArticleCreationCommand data, int authorId)
        {

            if (await _repository.IsExistArticle(SlugConverter.CreateSlug(data.title)))
                throw new ArgumentException("Article with such title or email alredy exists");

            var entity = Entities.Article.CreateArticle(data.title, data.description, data.body, authorId);
            var tags = await _createTags.CreateTagsList(data.tagList);
            entity.AddTags(tags);
            return await _repository.CreateArticleInDatabase(entity);


        }
    }
}
