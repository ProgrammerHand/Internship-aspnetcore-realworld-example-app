namespace Conduit.Features.Tags.Application.Interfaces
{
    public interface ICreate
    {
        Task CreateTags(List<string> names, int articleId);
    }
}
