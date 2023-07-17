using TecnicalSupportAppV1.Api.Models.Enums;

namespace TecnicalSupportAppV1.Api.Interfaces.Services
{
    public interface IRolesService
    {
        List<RolesEnum> GetRolesByUserName(string username);
        Task AddRoleByUserId(long userId, RolesEnum rol);
    }
}
