using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TecnicalSupportAppV1.Api.Interfaces.Authertification;
using TecnicalSupportAppV1.Api.Interfaces.Facades;
using TecnicalSupportAppV1.Api.Interfaces.Services;
using TecnicalSupportAppV1.Api.Models;
using TecnicalSupportAppV1.Api.Models.Enums;

namespace TecnicalSupportAppV1.Bussiness.Facades
{
    public class AuthFacade : IAuthFacade
    {
        private readonly IUserService _userService;
        private readonly IConfiguration _configuration;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IRolesService _rolesService;
        public AuthFacade(IUserService userService, IPasswordHasher passwordHasher, IConfiguration configuration, IRolesService rolesService)
        {
            _passwordHasher = passwordHasher;
            _userService = userService;
            _configuration = configuration;
            _rolesService = rolesService;
        }

        public async Task<bool> VerifyLogin(string password, string userName)
        {
            User user = await _userService.FindUserByNameAsync(userName);
            if(user == null)
            {
                return false;
            }
            return _passwordHasher.Verify(user.Password, password);
        }

        public string CreateJwtToken(string username)
        {
            List<RolesEnum> roles = _rolesService.GetRolesByUserName(username);

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, username),
            };
            AddRolesInToClaims(roles, claims);
            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_configuration.GetValue<string>("AppSettings:Token")));

            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);

            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddDays(1),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        protected void AddRolesInToClaims(List<RolesEnum> roles, List<Claim> claims)
        {
            roles.ForEach(x =>
            {
                claims.Add(new Claim(ClaimTypes.Role, x.ToString()));
            });
        }
        public async Task<string> Hash(string password)
        {
            return _passwordHasher.Hash(password);
        }
    }
}
