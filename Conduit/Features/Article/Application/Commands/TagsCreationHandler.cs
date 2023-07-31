using Conduit.Infrastructure.Repository.Interfaces;

namespace Conduit.Features.Article.Application.Commands
{
    public class TagsCreationHandler
    {
        private readonly ITagsRepository _repository;

        public TagsCreationHandler(ITagsRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Entities.Tag>> CreateTagsList(List<string> names) 
        {
            var tags = new List<Entities.Tag>();
            foreach (var tag in names ?? Enumerable.Empty<string>())
            {
                var existingTags = await _repository.GetTagsByName(names);
                var entity = existingTags.FirstOrDefault(t => t.TagName == tag);
                if (entity is null)
                    tags.Add(Entities.Tag.CreateTag(tag));
                else
                    tags.Add(entity);

            }
            return tags;
        }
    }
}
