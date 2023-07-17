using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Api.Interfaces.Services
{
    public interface IUserDao
    {
        Task<List<User>> GetUserListAsync();
        Task<User> CreateUserAsync(User User);
        Task<bool> IsUserNameInUsed(string user);
        Task<bool> IsUserCreatedById(long id);
        Task<bool> IsUserIdentificationInUsed(string userIdentificationNumber);
        Task UpdateUserAsync(User User);
        Task<User> FindUserById(long id);
        Task DeleteUserById(long id);
        Task<User> FindUserByNameAsync(string userName);
    }
}
