namespace Conduit.Entities
{
    public class Article
    {
        private Article(string title, string description, string body, ICollection<string>? tagList, User author)
        {
            Title = title;
            Description = description;
            Body = body;
            TagList = tagList;
            createdAt = DateTime.UtcNow;
            updatedAt = DateTime.UtcNow;
            AuthorId = author.Id;
            Author = author;
        }

        public Article()
        {
        }

        public int Id { get; private set; }
        public string? Slug { get; private set; }
        public string Title { get; private set; }
        public string Description { get; private set; }
        public string Body { get; private set; }
        public ICollection<string>? TagList { get; private set; }
        public DateTime createdAt { get; private set; }
        public DateTime updatedAt { get; private set; }
        public bool favorited { get; private set; }
        public int favoritesCount { get; private set; }
        public int AuthorId { get; private set; }
        public User? Author { get; private set; }

        public static Article CreateArticle(string title, string description, string body, ICollection<string>? TagList, User author)
        {
            return new Article(title, description, body, TagList, author);
        }
    }
}
