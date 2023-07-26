using Conduit.Features.Article.Application.Commands;
using System.ComponentModel.DataAnnotations;

namespace Conduit.Entities
{
    public class Tags
    {
        public int Id { get; private set; }
        public string TagName { get; private set; }
        public int ArticleId { get; private set; }
        public Article Article { get; private set; }

        private Tags(string name, int articleId)
        {
            TagName = name;
            ArticleId = articleId;
        }

        private Tags()
        {
        }

        public static Tags CreateTag (string name, int articleId) 
        {
            return new Tags(name, articleId);
        }

    }
}
