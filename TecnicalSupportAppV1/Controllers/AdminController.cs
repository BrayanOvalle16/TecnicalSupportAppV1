using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TecnicalSupportAppV1.Api.Interfaces.Facades;
using TecnicalSupportAppV1.Api.Interfaces.Services;
using TecnicalSupportAppV1.Api.Models;
using TecnicalSupportAppV1.Api.Models.Dtos;
using TecnicalSupportAppV1.Api.Models.Enums;
using TecnicalSupportAppV1.Bussiness.Facades;

namespace TecnicalSupportAppV1.Controllers
{
    [ApiController]
    [Route("api/service/admin")]
    [Authorize(Roles = "SuperAdmin")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService adminService;
        private readonly IMapper mapper;
        private readonly ILoginFacade _loginFacade;

        public AdminController(IAdminService _adminService, IMapper _mapper, ILoginFacade loginFacade)
        {
            mapper = _mapper;
            _loginFacade = loginFacade;
            adminService = _adminService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<Admin> adminList =  await adminService.GetAdminListAsync();
            return Ok(mapper.Map<List<AdminDto>>(adminList));
        }

        [HttpGet("find-by-id")]
        public async Task<ActionResult> GetById(long id)
        {
            Admin adminList = await adminService.FindAdminById(id);
            if(adminList == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<AdminDto>(adminList));
        }

        [HttpDelete()]
        public async Task<ActionResult> DeleteById(long id)
        {
            await adminService.DeleteAdminById(id);
            return Ok();
        }

/*        [HttpPut()]
        public async Task<ActionResult> UpdateById(AdminUpdateDto admin )
        {
            long userId = admin.UserId;
            if (userId == null)
            {
                return BadRequest("Please assign a Login to the Admin");
            }
            if (!await _loginFacade.IsUserCreatedByIdAsync(userId))
            {
                return BadRequest($"No Login associated to the login id {userId}");
            };

            var result = await adminService.CreateAdminAsync(mapper.Map<Admin>(admin));
            //Add Admin Role to User
            await _loginFacade.AddLoginRoleToUser(userId, RolesEnum.Admin);

            return Ok(mapper.Map<AdminDto>(admin));
        }*/

        [HttpPost]
        public async Task<ActionResult> Create(AdminCreationDto admin)
        {
            long userId = admin.UserId;
            if (userId == null)
            {
                return BadRequest("Please assign a Login to the Admin");
            }
            if(!await _loginFacade.IsUserCreatedByIdAsync(userId))
            {
                return BadRequest($"No Login associated to the login id {userId}");
            };

            if(await adminService.IsAdminAlreadyCreatedByUserId(userId))
            {
                return BadRequest($"Admin already associated with the login id {userId}");
            }
            var result = await adminService.CreateAdminAsync(mapper.Map<Admin>(admin));
            //Add Admin Role to User
            await _loginFacade.AddLoginRoleToUser(userId, RolesEnum.Admin);

            return Ok(mapper.Map<AdminDto>(result));
        }
    }
}
