using Conduit.Features.Tags.Application.Interfaces;

namespace Conduit.Features.Tags.Application.Queries
{
    public class GetUnique
    {
        private readonly ITagsRepository _repository;

        public record UniqueTagsEnvelop (List<string> tags);

        public GetUnique(ITagsRepository repository)
        {
            _repository = repository;
        }

        public async Task<UniqueTagsEnvelop> GetUniqueTags() 
        {
            return new UniqueTagsEnvelop(await _repository.GetUniqueTags());
        }
    }
}
