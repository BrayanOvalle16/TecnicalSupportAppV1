using TecnicalSupportAppV1.Api.Interfaces.Dao;
using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Data.Dao
{
    public class AdminDao : IAdminDao
    {
        private readonly DataContext _context;

        public AdminDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Admin>> GetAdminListAsync()
        {
            return await _context.Admins
                .Include(x => x.User)
                .ThenInclude(x => x.ContactInformation)
                .ToListAsync();
        }

        public async Task<Admin> CreateAdminAsync(Admin admin)
        {
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<Admin> UpdateAdminAsync(Admin admin)
        {
            _context.Update(admin);
            _context.Entry(admin).Property(x => x.User).IsModified = false;
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<Admin> FindAdminById(long id)
        {
            Admin admin = await _context.Admins
                .Include(x => x.User)
                .ThenInclude(x => x.ContactInformation)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return admin;
        }

        public async Task DeleteAdminById(long id)
        {
            Admin admin = await FindAdminById(id);
            _context.Remove(admin);
            await _context.SaveChangesAsync();
        }

        public Task<bool> IsAdminAlreadyCreatedByUserId(long userId)
        {
            return _context.Admins
                .AnyAsync(x => x.UserId == userId);
        }
    }
}
