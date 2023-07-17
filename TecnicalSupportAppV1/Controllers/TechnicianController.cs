using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TecnicalSupportAppV1.Api.Interfaces.Facades;
using TecnicalSupportAppV1.Api.Interfaces.Services;
using TecnicalSupportAppV1.Api.Models.Dtos;
using TecnicalSupportAppV1.Api.Models.Enums;
using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Controllers
{
    [ApiController]
    [Route("api/service/office/{officeId}/technician")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class TechnicianController : ControllerBase
    {
        private readonly ITechnicianService technicianService;
        private readonly IMapper mapper;
        private readonly ILoginFacade _loginFacade;

        public TechnicianController(ITechnicianService _technicianService, IMapper _mapper, ILoginFacade loginFacade)
        {
            mapper = _mapper;
            _loginFacade = loginFacade;
            technicianService = _technicianService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(long officeId)
        {
            List<Technician> technicianList = await technicianService.GetTechnicianListAsync(officeId);
            return Ok(mapper.Map<List<TechnicianDto>>(technicianList));
        }

        [HttpGet("find-by-id")]
        public async Task<ActionResult> GetById(long id, long officeId)
        {
            Technician technicianList = await technicianService.FindTechnicianById(id, officeId);
            if (technicianList == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<TechnicianDto>(technicianList));
        }

        [HttpDelete()]
        public async Task<ActionResult> DeleteById(long id, long officeId)
        {
            await technicianService.DeleteTechnicianById(id, officeId);
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> UpdateById(TechnicianUpdateDto technician, long officeId)
        {
            long userId = technician.UserId;
            if (userId == 0)
            {
                return BadRequest("Please assign a Login to the Technician");
            }
            Technician technicianDb = await technicianService.FindTechnicianById(technician.Id, officeId);
            if (technician.UserId != userId && !await _loginFacade.IsUserCreatedByIdAsync(userId))
            {
                return BadRequest($"No Login associated to the login id {userId}");
            };

            if (technician.UserId != userId && await technicianService.IsTechnicianAlreadyCreatedByUserId(userId, officeId))
            {
                return BadRequest($"Technician already associated with the login id {userId}");
            }
            var request = mapper.Map<Technician>(technician);
            request.OfficeId = officeId;
            var result = await technicianService.UpdateTechnicianAsync(request);

            return Ok(mapper.Map<TechnicianDto>(result));
        }

        [HttpPost]
        public async Task<ActionResult> Create(TechnicianCreationDto technician, long officeId)
        {
            long userId = technician.UserId;
            if (userId == null)
            {
                return BadRequest("Please assign a Login to the Technician");
            }
            if (!await _loginFacade.IsUserCreatedByIdAsync(userId))
            {
                return BadRequest($"No Login associated to the login id {userId}");
            };

            if (await technicianService.IsTechnicianAlreadyCreatedByUserId(userId, officeId))
            {
                return BadRequest($"Technician already associated with the login id {userId}");
            }
            var request = mapper.Map<Technician>(technician);
            request.OfficeId = officeId;
            var result = await technicianService.CreateTechnicianAsync(request);
            //Add Technician Role to User
            await _loginFacade.AddLoginRoleToUser(userId, RolesEnum.Technician);

            return Ok(mapper.Map<TechnicianDto>(result));
        }
    }
}

