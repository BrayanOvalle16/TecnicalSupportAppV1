using TecnicalSupportAppV1.Api.Interfaces.Services;
using TecnicalSupportAppV1.Api.Models;
using TecnicalSupportAppV1.Api.Models.Enums;

namespace TecnicalSupportAppV1.Bussiness.Services
{
    public class RolesService : IRolesService
    {
        private readonly IRolesDao _rolesDao;

        public RolesService(IRolesDao context)
        {
            _rolesDao = context;
        }

        public async Task AddRoleByUserId(long userId, RolesEnum rol)
        {
            await _rolesDao.AddRoleByUserIdAsync(userId, rol);
        }

        public List<RolesEnum> GetRolesByUserName(string username)
        {
            return _rolesDao.GetRolesByUserName(username);
        }
    }
}
