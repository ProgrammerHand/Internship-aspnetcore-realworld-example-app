namespace Conduit.Entities
{
    public class Article
    {
        private Article(string title, string description, string body, /*List<string>? tagList,*/ User author)
        {
            Title = title;
            Description = description;
            Body = body;
            //TagList = tagList;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            AuthorId = author.Id;
            //Author = author;
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
       // public List<string>? TagList { get; private set; }
        //public List<string> ArticleTags { get; set; }
        //public List<string> ArticleFavorites { get; set; } 
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }


        public static Article CreateArticle(string title, string description, string body, /*List<string>? TagList,*/ User author)
        {
            return new Article(title, description, body, /*TagList,*/ author);
        }
    }
}
