using TecnicalSupportAppV1.Api.Models.Enums;

namespace TecnicalSupportAppV1.Api.Interfaces.Facades
{
    public interface ILoginFacade
    {
        Task<bool> IsUserCreatedByIdAsync(long id);
        Task AddLoginRoleToUser(long userId, RolesEnum rol);
    }
}
