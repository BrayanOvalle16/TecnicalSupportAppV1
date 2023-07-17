using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Api.Interfaces.Services
{
    public interface IAdminService
    {
        Task<List<Admin>> GetAdminListAsync();
        Task<Admin> CreateAdminAsync(Admin Admin);
        Task<Admin> UpdateAdminAsync(Admin Admin);
        Task<Admin> FindAdminById(long id);
        Task DeleteAdminById(long id);
        Task<bool> IsAdminAlreadyCreatedByUserId(long userId);
    }
}
