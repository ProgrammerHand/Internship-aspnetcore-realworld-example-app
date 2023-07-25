using Conduit.Features.User.Application.Dto;
using Conduit.Features.User.Application.Interfaces;
using Conduit.Infrastructure.Security.Interface;


namespace Conduit.Features.User.Application.Queries
{
    public class GetCurrent
    {
        private readonly IUserRepository _repository;
        private readonly IJWTtoken _jvtService;


        public GetCurrent(IUserRepository repository, IJWTtoken jwtService)
        {
            _repository = repository;
            _jvtService = jwtService;
        }

        public async Task<AunthenticatedUser> GetCurrentUser(int id) 
        {
            var aunthUserData = await _repository.GetAunthUser(id);
            return new AunthenticatedUser { email = aunthUserData.email, token = _jvtService.CreateToken(aunthUserData.username, aunthUserData.role, aunthUserData.id.ToString()), username = aunthUserData.username, bio = aunthUserData.bio, image = aunthUserData.image};
        }
    }
}
