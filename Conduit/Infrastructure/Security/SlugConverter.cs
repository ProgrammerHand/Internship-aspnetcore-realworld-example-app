using Conduit.Infrastructure.Repository.Interfaces;
using System.Text.RegularExpressions;

namespace Conduit.Infrastructure.Security
{
    public class SlugConverter
    {
        public static string CreateSlug(string phrase)
        {
            //return slug.Replace(" ", "_");
            if (phrase is null)
            {
                return null;
            }
            string slug = phrase.ToLowerInvariant();
            // invalid chars
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");
            // convert multiple spaces into one space
            slug = Regex.Replace(slug, @"\s+", " ").Trim();
            // cut and trim
            slug = slug.Substring(0, slug.Length <= 45 ? slug.Length : 45).Trim();
            return Regex.Replace(slug, @"\s", "-"); // hyphens
        }
    }
}
