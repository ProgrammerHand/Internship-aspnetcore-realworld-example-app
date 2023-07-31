using Conduit.Infrastructure.Security;

namespace Conduit.Entities
{
    public class Article
    {
        private Article(string title, string description, string body, int authorId)
        {
            Title = title;
            Description = description;
            Body = body;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            AuthorId = authorId;
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
        public IEnumerable<Tag> Tags => TagsRelation;
        private ICollection<Tag> TagsRelation = new List<Tag>();
        public IEnumerable<Comment> Comments => CommentsRelation;
        private ICollection<Comment> CommentsRelation = new List<Comment>();
        //public List<string> ArticleFavorites { get; set; } 
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; private set; }


        public static Article CreateArticle(string title, string description, string body, int authorId)
        {
            var entity = new Article(title, description, body, authorId);
            entity.GenerateSlug();
            return entity;
        }

        private void GenerateSlug() 
        {
            Slug = SlugConverter.CreateSlug(Title);
        }

        public void AddTags(List<Tag> tags)
        {

            var sumAmount = tags.Count() + TagsRelation.Count();
            if (sumAmount > 10)
                throw new ArgumentException($"Cannot add more than 10 tags to an article. You tryed to add {sumAmount}.");
            var existedTagIds = TagsRelation.Select(tag => tag.Id).ToList();
            foreach (var tag in tags)
            {
                if (existedTagIds.Contains(tag.Id) == false)
                    TagsRelation.Add(tag);
            }
        }

        public void DeleteTags(List<string> tags)
        {
            foreach (var tag in tags)
            {
                    TagsRelation.Remove(TagsRelation.First(x => x.TagName == tag));
            }
        }

        public void AddComent(Comment comment) 
        {
            CommentsRelation.Add(comment);
        }
    }
}
