namespace Conduit.Entities
{
    public class Article
    {
        public int Id { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Body { get; set; }
        public ICollection<string> TagList { get; set; }
        public int AuthorId { get; set; }
        public User Author { get; set; }
    }
}
