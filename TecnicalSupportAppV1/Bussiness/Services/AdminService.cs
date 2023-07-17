using Microsoft.EntityFrameworkCore;
using TecnicalSupportAppV1.Api.Interfaces.Dao;
using TecnicalSupportAppV1.Api.Interfaces.Services;
using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Bussiness.Services
{
    public class AdminService : IAdminService
    {
        private readonly IAdminDao _adminDao;

        public AdminService(IAdminDao context)
        {
            _adminDao = context;
        }

        public async Task<List<Admin>> GetAdminListAsync()
        {
            return await _adminDao.GetAdminListAsync();
        }

        public async Task<Admin> CreateAdminAsync(Admin admin)
        {
            if (admin != null)
            {
                await _adminDao.CreateAdminAsync(admin);
            }
            return admin;
        }

        public async Task<Admin> UpdateAdminAsync(Admin admin)
        {
            await _adminDao.UpdateAdminAsync(admin);
            return admin;
        }

        public async Task<Admin> FindAdminById(long id)
        {
            return  await _adminDao.FindAdminById(id);
        }

        public async Task DeleteAdminById(long id)
        {
            await _adminDao.DeleteAdminById(id);
        }

        public Task<bool> IsAdminAlreadyCreatedByUserId(long userId)
        {
            return _adminDao.IsAdminAlreadyCreatedByUserId(userId);
        }
    }
}
