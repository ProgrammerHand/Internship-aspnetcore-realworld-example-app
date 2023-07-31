using Conduit.Features.Article.Application.Commands;
using System.ComponentModel.DataAnnotations;

namespace Conduit.Entities
{

    public class Tag
    {
        public int Id { get; private set; }
        public string TagName { get; private set; }
        private ICollection<Article> _articles = new List<Article>();
        public IEnumerable<Article> Articles => _articles;

        private Tag(string name)
        {
            TagName = name;
        }

        private Tag()
        {
        }

        public static Tag CreateTag(string name) 
        {
            return new Tag(name);
        }
    }
}
