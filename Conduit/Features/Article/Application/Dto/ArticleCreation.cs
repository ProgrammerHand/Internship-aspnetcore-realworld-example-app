namespace Conduit.Features.Article.Application.Dto
{
    public class ArticleCreation
    {
        public string title { get; set; }
        public string description { get; set; }
        public string body { get; set; }
        public ICollection<string>? tagList { get; set; } 
    }
}
