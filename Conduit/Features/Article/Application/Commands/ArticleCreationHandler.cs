using Conduit.Infrastructure.Repository.Interfaces;

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
        private readonly CreateTagsHandler _createTags;

        public ArticleCreationHandler(IArticleRepository repository, CreateTagsHandler createTagsServ)
        {
            _repository = repository;
            _createTags = createTagsServ;
        }

        public async Task<bool> CreateArticle(ArticleCreationCommand data, int authorId)
        {

            if (await _repository.IsExisArticle(data.title))
                throw new ArgumentException("Article with such title or email alredy exists");

            var entity = Entities.Article.CreateArticle(data.title, data.description, data.body, authorId);
            var tags = await _createTags.CreateTagsList(data.tagList);
            entity.AddTags(tags);
            await _repository.CreateArticleInDatabase(entity);
            return await _repository.Save();
               

        }
    }
}
