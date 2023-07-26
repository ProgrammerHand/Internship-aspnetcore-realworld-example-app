using Conduit.Features.Tags.Application.Interfaces;

namespace Conduit.Features.Tags.Application.Commands
{
    public class Add
    {
        private readonly ITagsRepository _repository;

        public Add(ITagsRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> AddTags(List<string> names, int articleId)
        {
            if (names.Count < 10)
            {
                foreach (var tag in (names ?? Enumerable.Empty<string>()))
                {
                    var t = await _repository.GetArticleTag(tag, articleId);
                    if (t == null)
                    {
                        await _repository.CreateTag(Entities.Tags.CreateTag(tag, articleId));
                    }
                }
                return await _repository.Save();
            }
            else
                throw new ArgumentException("Article with such title or email alredy exists");
        }
    }
}
