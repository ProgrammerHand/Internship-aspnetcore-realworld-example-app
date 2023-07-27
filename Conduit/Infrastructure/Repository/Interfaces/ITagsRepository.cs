using Conduit.Entities;

namespace Conduit.Infrastructure.Repository.Interfaces
{
    public interface ITagsRepository
    {
        Task<Tags> GetTag(string name);
        Task<List<Tags>> GetTagsByName(IEnumerable<string> names);

        Task CreateTag(Tags tag);

        Task<bool> Save();
    }
}
