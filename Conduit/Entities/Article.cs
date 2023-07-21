namespace Conduit.Entities
{
    public class Article
    {
        public int id { get; set; }
        public string slug { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public string Body { get; set; }
        public List<string> tagList { get; set; }
        public int authorId { get; set; }
        public User Author { get; set; }
    }
}
