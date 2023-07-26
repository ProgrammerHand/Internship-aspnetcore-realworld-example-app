namespace Conduit.Entities
{
    public class Article
    {
        private Article(string title, string description, string body, User author)
        {
            Title = title;
            Description = description;
            Body = body;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            AuthorId = author.Id;
        }

        public Article()
        {
        }

        public int Id { get; private set; }
        public string? Slug { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Body { get; private set; }
        public int AuthorId { get; private set; }
        public User Author { get; private set; }
        public bool Favorited { get; private set; }
        public int FavoritesCount { get; private set; }
        public List<string> TagList => TagsRelation.Where(x => x.ArticleId == Id).Select(x => x.TagName!).ToList();
        public List<Tags> TagsRelation { get; private set; } = new();
        //public List<string> ArticleFavorites { get; set; } 
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }


        public static Article CreateArticle(string title, string description, string body, User author)
        {
            return new Article(title, description, body, author);
        }
    }
}
