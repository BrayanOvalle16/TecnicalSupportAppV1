using TecnicalSupportAppV1.Api.Models.Enums;

namespace TecnicalSupportAppV1.Api.Interfaces.Services
{
    public interface IRolesDao
    {
        List<RolesEnum> GetRolesByUserName(string username);
        Task AddRoleByUserIdAsync(long userId, RolesEnum rol);
    }
}
