namespace Conduit.Features.Article.Application.Dto
{
    public class ArticleFeed
    { 
    
        public string? slug { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string body { get; set; }
        public IEnumerable<string> tagList { get; set; }
        public bool favorited { get; set; }
        public int favoritesCount { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public Entities.User author { get; set; }

    }

    public record ArticleFeedEnvelop(ICollection<ArticleFeed> articles, int articlesCount);
}
