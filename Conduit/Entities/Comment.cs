namespace Conduit.Entities
{
    public class Comment
    {
        public int Id { get; private set; }

        public string Body { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public int ArticleId { get; private set; }

        public User? Author { get; private set; }

        private Comment(string body, User author)
        {
            Body = body;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Author = author;
        }

        private Comment()
        {
            
        }

        public static Comment CreateComment (string body, User author)
        {
            return new Comment(body, author);
        }


    }

}
