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
        private readonly CreateTags _createTags;

        public ArticleCreationHandler(IArticleRepository repository, CreateTags createTagsServ)
        {
            _repository = repository;
            _createTags = createTagsServ;
        }

        public async Task<bool> CreateArticle(ArticleCreationCommand data, int authorId)
        {

            if (await _repository.IsExisArticle(data.title))
                throw new ArgumentException("Article with such title or email alredy exists");
            var entity = Entities.Article.CreateArticle(data.title, data.description, data.body, await _repository.GetUserById(authorId));
            var tags = await _createTags.CreateTagsList(data.tagList);
            entity.SetTags(tags);
            await _repository.CreateArticleDatabase(entity);
            return await _repository.Save();
               

        }
    }
}
