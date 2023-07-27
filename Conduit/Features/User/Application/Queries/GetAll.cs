using Conduit.Infrastructure.Repository.Interfaces;

namespace Conduit.Features.User.Application.Queries
{
    public class GetAll
    {
        private readonly IUserRepository _repository;

        public GetAll(IUserRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<Entities.User>> GetAllUsers()
        {
            return await _repository.GetAllUsers();
        }
    }
}
