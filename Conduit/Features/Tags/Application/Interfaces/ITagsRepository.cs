using Conduit.Entities;

namespace Conduit.Features.Tags.Application.Interfaces
{
    public interface ITagsRepository
    {
        Task<Entities.Tags> GetTag(string name);

        Task<Entities.Tags> GetArticleTag(string name, int articleId);

        Task CreateTag (Entities.Tags tag);

        Task<List<string>> GetUniqueTags();

        Task<bool> Save();
    }
}
