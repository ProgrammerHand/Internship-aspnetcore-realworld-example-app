using Conduit.Features.Article.Application.Commands;

namespace Conduit.Features.Article.WebApp
{
        public record AddCommentEnvelop(AddCommentCommand comment);
}
