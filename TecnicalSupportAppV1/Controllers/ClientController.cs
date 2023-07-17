using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TecnicalSupportAppV1.Api.Interfaces.Facades;
using TecnicalSupportAppV1.Api.Interfaces.Services;
using TecnicalSupportAppV1.Api.Models.Dtos;
using TecnicalSupportAppV1.Api.Models.Enums;
using TecnicalSupportAppV1.Api.Models;
using Microsoft.AspNetCore.Authorization;

namespace TecnicalSupportAppV1.Controllers
{
    [ApiController]
    [Route("api/service/client")]
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class ClientController : ControllerBase
    {
        private readonly IClientService clientService;
        private readonly IMapper mapper;
        private readonly ILoginFacade _loginFacade;

        public ClientController(IClientService _clientService, IMapper _mapper, ILoginFacade loginFacade)
        {
            mapper = _mapper;
            _loginFacade = loginFacade;
            clientService = _clientService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<Client> clientList = await clientService.GetClientListAsync();
            return Ok(mapper.Map<List<ClientDto>>(clientList));
        }

        [HttpGet("find-by-id")]
        public async Task<ActionResult> GetById(long id)
        {
            Client clientList = await clientService.FindClientById(id);
            if (clientList == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ClientDto>(clientList));
        }

        [HttpDelete()]
        public async Task<ActionResult> DeleteById(long id)
        {
            await clientService.DeleteClientById(id);
            return Ok();
        }

        /*        [HttpPut()]
                public async Task<ActionResult> UpdateById(ClientUpdateDto client )
                {
                    long userId = client.UserId;
                    if (userId == null)
                    {
                        return BadRequest("Please assign a Login to the Client");
                    }
                    if (!await _loginFacade.IsUserCreatedByIdAsync(userId))
                    {
                        return BadRequest($"No Login associated to the login id {userId}");
                    };

                    var result = await clientService.CreateClientAsync(mapper.Map<Client>(client));
                    //Add Client Role to User
                    await _loginFacade.AddLoginRoleToUser(userId, RolesEnum.Client);

                    return Ok(mapper.Map<ClientDto>(client));
                }*/

        [HttpPost]
        public async Task<ActionResult> Create(ClientCreationDto client)
        {
            long userId = client.UserId;
            if (userId == null)
            {
                return BadRequest("Please assign a Login to the Client");
            }
            if (!await _loginFacade.IsUserCreatedByIdAsync(userId))
            {
                return BadRequest($"No Login associated to the login id {userId}");
            };

            if (await clientService.IsClientAlreadyCreatedByUserId(userId))
            {
                return BadRequest($"Client already associated with the login id {userId}");
            }
            var result = await clientService.CreateClientAsync(mapper.Map<Client>(client));
            //Add Client Role to User
            await _loginFacade.AddLoginRoleToUser(userId, RolesEnum.Client);

            return Ok(mapper.Map<ClientDto>(result));
        }
    }
}
