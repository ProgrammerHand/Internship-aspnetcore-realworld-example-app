namespace Conduit.Features.Article.Application.Dto
{
    public class ArticleCreation
    {
        public string title { get; set; }
        public string description { get; set; }
        public string body { get; set; }
        public List<string>? tagList { get; set; }
    }

    public record ArticleCreationEnvelop(ArticleCreation article);
}
