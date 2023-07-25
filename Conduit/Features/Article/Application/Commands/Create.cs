using Conduit.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Conduit.Features.Article.Application.Dto;
using Conduit.Features.Article.Application.Interfaces;

namespace Conduit.Features.Article.Application.Commands
{
    public class Create
    {
        private readonly IArticleRepository _repository;

        public Create(IArticleRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> CreateArticle(ArticleCreation data, int authorId)
        {

            if (!await _repository.IsExisArticle(data.title))
            {
                return await _repository.CreateArticleDatabase(data, authorId);
            }
            else
                throw new ArgumentException("Article with such title or email alredy exists");

        }
    }
}
