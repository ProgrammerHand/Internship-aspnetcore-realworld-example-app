using Conduit.Features.User.Application.Dto;
using Conduit.Infrastructure.Security.Interface;
using Conduit.Infrastructure;
using Conduit.Infrastructure.Repository.Interfaces;

namespace Conduit.Features.User.Application.Commands
{
    public class UserRegistrationData
    {
        public string username { get; init; }
        public string email { get; init; }
        public string password { get; init; }
    }

    public class RegistrationHandler
    {
        private readonly IHashingService _hashingService;
        private readonly IUserRepository _repository;

        public RegistrationHandler(ConduitContext context, IHashingService hashingService, IUserRepository repository)
        {
            _repository = repository;
            _hashingService = hashingService;
        }


        public async Task<bool> Register(UserRegistrationData data)
        {
            if (await _repository.IsExistUser(data.email, data.username) == false)
            {
                var hash = _hashingService.HashPassword(data.password);
                await _repository.CreateUser(Entities.User.CreateUser(data.email, data.username, hash.Item1, hash.Item2));
                return await _repository.Save();
            }
            else
                throw new ArgumentException("User with such username or email alredy exists");

        }
    }
}
