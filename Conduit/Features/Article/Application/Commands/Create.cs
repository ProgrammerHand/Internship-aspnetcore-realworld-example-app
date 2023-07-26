using Conduit.Features.Article.Application.Dto;
using Conduit.Features.Article.Application.Interfaces;
using Conduit.Features.Tags.Application.Interfaces;

namespace Conduit.Features.Article.Application.Commands
{
    public class Create
    {
        private readonly IArticleRepository _repository;
        private readonly ICreate _tags;


        public Create(IArticleRepository repository, ICreate tags)
        {
            _repository = repository;
            _tags = tags;
        }

        public async Task<bool> CreateArticle(ArticleCreation data, int authorId)
        {

            if (!await _repository.IsExisArticle(data.title))
            {
                await _repository.CreateArticleDatabase(Entities.Article.CreateArticle(data.title, data.description, data.body, await _repository.GetUserById(authorId)));
                await _repository.Save();
                var entity = await _repository.GetArticleByTitle(data.title); 
                await _tags.CreateTags(data.tagList, entity.Id);
                return await _repository.Save();
            }
            else
                throw new ArgumentException("Article with such title or email alredy exists");

        }
    }
}
