using Conduit.Features.User.Application.Dto;
using Conduit.Infrastructure.Security.Interface;
using Conduit.Infrastructure;
using Conduit.Features.User.Application.Interfaces;

namespace Conduit.Features.User.Application.Commands
{
    public class Registration
    {
        private readonly IHashingService _hashingService;
        private readonly IUserRepository _repository;

        public record UserRegisterCredentials
        {
            public string username { get; init; }
            public string email { get; init; }
            public byte[] passwordHash { get; init; }
        }

        public Registration(ConduitContext context, IHashingService hashingService, IUserRepository repository)
        {
            _repository = repository;
            _hashingService = hashingService;
        }


        public async Task<bool> Register(UserRegistrationData data)
        {

            if (!await _repository.IsExistUser(data.email, data.username))
            {
                var hash = _hashingService.HashPassword(data.password);
                return await _repository.CreateUser(Entities.User.CreateUser(data.email, data.username, hash.Item1, hash.Item2));
            }
            else
                throw new ArgumentException("User with such username or email alredy exists");

        }
    }
}
