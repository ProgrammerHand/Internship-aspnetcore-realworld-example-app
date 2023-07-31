using Conduit.Infrastructure.Repository.Interfaces;
using Conduit.Features.Article.Application.Commands;

namespace Conduit.Features.Article.Application.Commands
{
    public class AddCommentCommand
    {
        public string body { get; init; }
    }

    public class AddCommentHandler
    {
        private readonly IArticleRepository _repositoryArticle;
        private readonly CommentCreationHandler _commentCreation;

        public AddCommentHandler(IArticleRepository repositoryArticle, CommentCreationHandler commentCreationServ)
        {
            _repositoryArticle = repositoryArticle;
            _commentCreation = commentCreationServ;
        }

        public async Task<bool> AddCommentoArticle(AddCommentCommand data, string slug, int authorId)
        {
            var entity = await _repositoryArticle.GetArticleBySlug(slug);
            var comment = await _commentCreation.CreateComment(data.body, authorId);
            entity.AddComent(comment);  
            return await _repositoryArticle.UpdateArticle(entity);
        }
    }
}
