using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TecnicalSupportAppV1.Api.Models;
using TecnicalSupportAppV1.Api.Models.Dtos;
using TecnicalSupportAppV1.Api.Interfaces.Authertification;
using Microsoft.AspNetCore.Authorization;
using TecnicalSupportAppV1.Api.Models.Enums;
using System.Data;
using TecnicalSupportAppV1.Api.Interfaces.Services;
using TecnicalSupportAppV1.Api.Interfaces.Facades;

namespace TecnicalSupportAppV1.Controllers
{
    [ApiController]
    [Route("api/service/auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper mapper;
        private readonly IAuthFacade authFacade;
        public AuthController(IUserService _UserService, IMapper _mapper, IAuthFacade _authFacade)
        {
            authFacade = _authFacade;
            mapper = _mapper;
            userService = _UserService;
        }

        [HttpGet, Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> GetAll()
        {
            List<User> UserList = await userService.GetUserListAsync();
            return Ok(mapper.Map<List<UserDto>>(UserList));
        }

        [HttpGet("find-by-id"), Authorize(Roles = "Admin")]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> GetById(long id)
        {
           User UserList = await userService.FindUserById(id);
 
            if (UserList == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<UserDto>(UserList));
        }

        [HttpPost]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> Create(UserCreationDto user)
        {
            //Encrypting Password
            user.Password = await authFacade.Hash(user.Password);

            if (await userService.IsUserNameInUsed(user.Username))
            {
                return BadRequest("UserName already in used.");
            }
            
            if (await userService.IsUserIdentificationInUsed(user.ContactInformation?.IdentificationNumber))
            {
                return BadRequest("Identification number already in used.");
            }

            var result = await userService.CreateUserAsync(mapper.Map<User>(user));
            return Ok(mapper.Map<UserDto>(result));
        }


        [HttpPost("login")]
        public async Task<ActionResult> Login(UserLoginDto user)
        {
            if(!await authFacade.VerifyLogin(user.Password, user.Username))
            {
                return BadRequest("User Not Found Or Incorrect Password");
            }
            //Generate Token
            String token = authFacade.CreateJwtToken(user.Username);
            UserJwtDto tokenResponse = new UserJwtDto();
            tokenResponse.Token = token;

            return Ok(tokenResponse);
        }

        [HttpPut]
        [Authorize(Roles = "SuperAdmin")]
        public async Task<ActionResult> Update(UserDto userDto)
        {
            User user = await userService.FindUserById(userDto.Id);
            if(user == null)
            {
                return NotFound();
            }
            String username = userDto.Username ?? user.Username;
            if (user.Username != username && await userService.IsUserNameInUsed(username))
            {
                return BadRequest("UserName already in used.");
            }

            String identification = userDto.ContactInformation?.IdentificationNumber ?? user.ContactInformation?.IdentificationNumber;
            if (identification != user.ContactInformation?.IdentificationNumber && 
                await userService.IsUserIdentificationInUsed(identification))
            {
                return BadRequest("Identification number already in used.");
            }

            await userService.UpdateUserAsync(mapper.Map<User>(userDto));
            return Ok();
        }
    }
}
