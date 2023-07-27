using Conduit.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Conduit.Infrastructure
{
    public class ArticleConfiguration
    {
        public void Configure(EntityTypeBuilder<Article> builder)
        {
            builder.HasMany(a => a.Tags).WithMany(t => t.Articles);
        }
    }
}
