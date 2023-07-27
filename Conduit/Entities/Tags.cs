using Conduit.Features.Article.Application.Commands;
using System.ComponentModel.DataAnnotations;

namespace Conduit.Entities
{

    public class Tags
    {
        public int Id { get; private set; }
        public string TagName { get; private set; }
        private ICollection<Article> _articles = new List<Article>();
        public IEnumerable<Article> Articles => _articles.ToList().AsReadOnly();

        private Tags(string name)
        {
            TagName = name;
        }

        private Tags()
        {
        }

        public static Tags CreateTag(string name) 
        {
            return new Tags(name);
        }
    }
}
