using Conduit.Features.User.Application.Dto;
using Conduit.Infrastructure.Repository.Interfaces;
using Conduit.Infrastructure.Security.Interface;

namespace Conduit.Features.User.Application.Commands
{
    public class Update
    {
        private readonly IUserRepository _repository;
        private readonly IHashingService _hashingService;
        private readonly IJWTtoken _jvtService;
        public Update(IUserRepository repository, IHashingService hashingService, IJWTtoken jwtService)
        {
            _repository = repository;
            _hashingService = hashingService;
            _jvtService = jwtService;
        }

        public async Task<AunthenticatedUserEnvelop> UpdateUser(UserUpdateData data, int id )
        {
            if (!await _repository.IsExistUser(id, data.email, data.username))
            {
                var user = await _repository.GetUser(id);
                var newHashsalt = _hashingService.HashPassword(data.password);
                user.UpdateUser(data, newHashsalt.Item1, newHashsalt.Item2);
                await _repository.UpdateUserDatabase(user);
                return new AunthenticatedUserEnvelop(new AunthenticatedUser { email = user.Email, token = _jvtService.CreateToken(user.Username, user.Role, user.Id.ToString()), username = user.Username, bio = user.Bio, image = user.Image }); 
            }
            else
                throw new ArgumentException("User with such email or username alredy exist");
        }
    }
}
