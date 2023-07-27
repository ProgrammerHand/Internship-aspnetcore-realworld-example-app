using Conduit.Features.User.Application.Dto;
using Conduit.Infrastructure.Repository.Interfaces;
using Conduit.Infrastructure.Security.Interface;

namespace Conduit.Features.User.Application.Commands
{
    public class Authentication
    {
        private readonly IUserRepository _repository;
        private readonly IHashingService _hashingService;
        private readonly IJWTtoken _jvtService;

        public Authentication(IHashingService hashingService, IJWTtoken jwtService, IUserRepository repository)
        {
            _repository = repository;
            _hashingService = hashingService;
            _jvtService = jwtService;
        }
  

        public async Task<AunthenticatedUserEnvelop> Authenticate(UserAuthenticationData data)
        {
            if (_hashingService.VerifyPassword(data.Password, await _repository.GetPasswordHash(data.Email)))
            {
                var aunthUserData =  await _repository.GetAunthUser(data.Email);
                return new AunthenticatedUserEnvelop(new AunthenticatedUser
                {
                    email = aunthUserData.email,
                    username = aunthUserData.username,
                    token =_jvtService.CreateToken(aunthUserData.username, aunthUserData.role, aunthUserData.id.ToString()),
                    bio = aunthUserData.bio,
                    image = aunthUserData.image
                });
            }
            else
                throw new ArgumentException("Wrong password or email");

        }
    }
}
