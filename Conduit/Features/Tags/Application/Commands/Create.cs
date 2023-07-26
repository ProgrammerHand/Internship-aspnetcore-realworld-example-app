using Conduit.Entities;
using Conduit.Features.Tags.Application.Interfaces;

namespace Conduit.Features.Tags.Application.Commands
{
    public class Create : ICreate
    {
        private readonly ITagsRepository _repository;

        public Create(ITagsRepository repository)
        {
            _repository = repository;
        }

        public async Task CreateTags(List<string> names, int articleId)
        {
            foreach (var tag in (names ?? Enumerable.Empty<string>()))
            {
                var t = await _repository.GetArticleTag(tag, articleId);
                if (t == null) 
                {
                    await _repository.CreateTag(Entities.Tags.CreateTag(tag, articleId));
                }
            }
        }
    }
}
