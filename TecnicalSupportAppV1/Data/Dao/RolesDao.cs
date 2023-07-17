using TecnicalSupportAppV1.Api.Interfaces.Services;
using TecnicalSupportAppV1.Api.Models;
using TecnicalSupportAppV1.Api.Models.Enums;

namespace TecnicalSupportAppV1.Bussiness.Services
{
    public class RolesDao : IRolesDao
    {
        private readonly DataContext _context;

        public RolesDao(DataContext context)
        {
            _context = context;
        }

        public async Task AddRoleByUserIdAsync(long userId, RolesEnum rol)
        {
            User user = await _context.Users
                .Include(x => x.Roles)
                .FirstOrDefaultAsync(x => x.Id == userId);
            if(user != null)
            {
                Roles rolEntity  = await _context.Roles
                    .FirstOrDefaultAsync(x => x.Rol == rol);
                user.Roles.Add(rolEntity);
               // rolEntity.Users = new List<User>() { user };
                await _context.SaveChangesAsync();
            }
        }

        public List<RolesEnum> GetRolesByUserName(string username)
        {
            List<Roles> roles = _context.Users
                .Include(x => x.Roles)
                .FirstOrDefault(x => x.Username == username).Roles;

            if(roles == null)
            {
                return new List<RolesEnum>();
            }
            return roles.Select(x => x.Rol).ToList();
        }

    }
}
