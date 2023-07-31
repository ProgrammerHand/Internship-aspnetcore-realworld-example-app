using Conduit.Entities;
using Conduit.Infrastructure.Repository.Interfaces;

namespace Conduit.Features.Article.Application.Commands
{
    public class CommentCreationHandler
    {
        private readonly IUserRepository _userRepository;

        public CommentCreationHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Comment> CreateComment(string body, int authorId) 
        {
            return Comment.CreateComment(body, await _userRepository.GetUser(authorId));
        }
    }
}
