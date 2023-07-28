using Conduit.Features.User.Application.Dto;
using Conduit.Infrastructure.Security.Interface;

namespace Conduit.Infrastructure.Repository.Interfaces
{
    public interface IUserRepository
    {
        Task<UserAunthCredentials> GetPasswordHash(string email);
        Task<AunthenticatedUserData> GetAunthUser(string email);
        Task<AunthenticatedUserData> GetAunthUser(int id);
        Task<Entities.User> GetUser(int id);
        Task<List<Entities.User>> GetAllUsers();
        Task UpdateUserDatabase(Entities.User user);
        Task<bool> IsExistUser(int id, string email, string username);
        Task<bool> IsExistUser(string email, string username);
        Task CreateUser(Entities.User newUser);
        Task<bool> Save();
    }
}
