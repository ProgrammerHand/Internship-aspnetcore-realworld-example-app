namespace Conduit.Entities
{
    public class Comments
    {
        public int Id { get; private set; }

        public string Body { get; private set; }

        public DateTime CreatedAt { get; private set; }

        public DateTime UpdatedAt { get; private set; }

        public User Author { get; private set; }

        private Comments(string body, User author)
        {
            Body = body;
            CreatedAt = DateTime.UtcNow;
            UpdatedAt = DateTime.UtcNow;
            Author = author;
        }

        private Comments()
        {
            
        }

        public Comments CreateComment (string body, User author)
        {
            return new Comments(body, author);
        }


    }

}
