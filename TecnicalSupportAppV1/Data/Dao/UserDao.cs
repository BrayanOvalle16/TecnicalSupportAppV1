using TecnicalSupportAppV1.Api.Models;
using TecnicalSupportAppV1.Api.Interfaces.Services;

namespace TecnicalSupportAppV1.Bussiness.Services
{
    public class UserDao : IUserDao
    {
        private readonly DataContext _context;

        public UserDao(DataContext context)
        {
            _context = context;
        }
        public async Task<List<User>> GetUserListAsync()
        {
            return await _context.Users
                .Include(x => x.ContactInformation)
                .Include(x => x.Roles)
                .ToListAsync();
        }

        public async Task<User> CreateUserAsync(User User)
        {
            if (User != null)
            {
                _context.Add(User);
                await _context.SaveChangesAsync();
            }
            return User;
        }

        public async Task UpdateUserAsync(User User)
        {
            _context.Update(User);
            _context.Entry(User).Property(x => x.Password).IsModified = false;
            await _context.SaveChangesAsync();
        }

        public async Task<User> FindUserById(long id)
        {
            User User = await _context.Users
                .Include(x => x.ContactInformation)
                .Include(x => x.Roles)
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return User;
        }

        public async Task DeleteUserById(long id)
        {
            User User = await FindUserById(id);
            _context.Remove(User);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> IsUserNameInUsed(string username)
        {
            return await _context.Users
                .Where(x => x.Username == username)
                .AnyAsync();
        }

        public async Task<bool> IsUserIdentificationInUsed(string userIdentificationNumber)
        {
            return await _context.ContactInformations
                .Where(x => x.IdentificationNumber == userIdentificationNumber)
                .AnyAsync();
        }

        public async Task<User> FindUserByNameAsync(string userName)
        {
            User User = await _context.Users
                .Where(x => x.Username.Equals(userName))
                .FirstOrDefaultAsync();
            return User;
        }

        public async Task<bool> IsUserCreatedById(long id)
        {
            return await _context.Users
                .Where(x => x.Id == id)
                .AnyAsync();
        }
    }
}

