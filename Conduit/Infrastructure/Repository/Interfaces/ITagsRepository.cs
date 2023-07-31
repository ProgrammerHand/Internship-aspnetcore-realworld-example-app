using Conduit.Entities;

namespace Conduit.Infrastructure.Repository.Interfaces
{
    public interface ITagsRepository
    {
        Task<Tag> GetTag(string name);
        Task<List<Tag>> GetTagsByName(IEnumerable<string> names);

        Task CreateTag(Tag tag);

        Task<bool> Save();
    }
}
