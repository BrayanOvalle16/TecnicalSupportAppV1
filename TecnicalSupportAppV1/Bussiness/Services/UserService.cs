using TecnicalSupportAppV1.Api.Models;
using TecnicalSupportAppV1.Api.Interfaces.Services;

namespace TecnicalSupportAppV1.Bussiness.Services
{
    public class UserService : IUserService
    {
        private readonly IUserDao _userDao;

        public UserService(IUserDao context)
        {
            _userDao = context;
        }
        public async Task<List<User>> GetUserListAsync()
        {
            return await _userDao.GetUserListAsync();
        }

        public async Task<User> CreateUserAsync(User User)
        {
            if (User != null)
            {
                _userDao.CreateUserAsync(User);
            }
            return User;
        }

        public async Task UpdateUserAsync(User User)
        {
            await _userDao.UpdateUserAsync(User);
        }

        public async Task<User> FindUserById(long id)
        {
            User User = await _userDao.FindUserById(id);
            return User;
        }

        public async Task DeleteUserById(long id)
        {
            await _userDao.DeleteUserById(id);
        }

        public async Task<bool> IsUserNameInUsed(string username)
        {
            if (username == null)
            {
                return false;
            }
            return await _userDao.IsUserNameInUsed(username);
        }

        public async Task<bool> IsUserIdentificationInUsed(string userIdentificationNumber)
        {
            if (userIdentificationNumber == null)
            {
                return false;
            }
            return await _userDao.IsUserIdentificationInUsed(userIdentificationNumber);
        }

        public async Task<User> FindUserByNameAsync(string userName)
        {
            User User = await _userDao.FindUserByNameAsync(userName);
            return User;
        }

        public async Task<bool> IsUserCreatedById(long id)
        {
            if (id != null)
            {
                return await _userDao.IsUserCreatedById(id);
            }
            return false;
        }
    }
}
