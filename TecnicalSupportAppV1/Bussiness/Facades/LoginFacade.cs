using Microsoft.AspNetCore.Identity;
using TecnicalSupportAppV1.Api.Interfaces.Facades;
using TecnicalSupportAppV1.Api.Interfaces.Services;
using TecnicalSupportAppV1.Api.Models.Enums;

namespace TecnicalSupportAppV1.Bussiness.Facades
{
    public class LoginFacade: ILoginFacade
    {
        public readonly IUserService _userService;
        public readonly IRolesService _rolesService;

        public LoginFacade(IUserService userService, IRolesService rolesService)
        {
            _userService = userService;
            _rolesService = rolesService;
        }

        public async Task AddLoginRoleToUser(long userId, RolesEnum rol)
        {
            await _rolesService.AddRoleByUserId(userId, rol);
        }

        public async Task<bool> IsUserCreatedByIdAsync(long id)
        {
            return await _userService.IsUserCreatedById(id);
        }
    }
}
