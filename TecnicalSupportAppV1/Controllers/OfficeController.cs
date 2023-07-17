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
    [Route("api/service/office")]
    [Authorize(Roles = "SuperAdmin")]
    public class OfficeController : ControllerBase
    {
        private readonly IOfficeService officeService;
        private readonly IMapper mapper;

        public OfficeController(IOfficeService _officeService, IMapper _mapper)
        {
            mapper = _mapper;
            officeService = _officeService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            List<Office> officeList = await officeService.GetOfficeListAsync();
            return Ok(mapper.Map<List<OfficeDto>>(officeList));
        }

        [HttpGet("find-by-id")]
        public async Task<ActionResult> GetById(long id)
        {
            Office officeList = await officeService.FindOfficeById(id);
            if (officeList == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<OfficeDto>(officeList));
        }

        [HttpDelete()]
        public async Task<ActionResult> DeleteById(long id)
        {
            await officeService.DeleteOfficeById(id);
            return Ok();
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateById(OfficeDto office)
        {
            if(office.Id == null)
            {
                return BadRequest($"Id not set in the request");
            }
            Office officeBeforeUpdate = await officeService.FindOfficeById(office.Id);
            if (officeBeforeUpdate.Name != office.Name &&  await officeService.IsOfficeAlreadyCreatedByName(office.Name))
            {
                return BadRequest($"Name {office.Name} is already associated to a Office, please chose other");
            }
            var result = await officeService.UpdateOfficeAsync(mapper.Map<Office>(office));

            return Ok(mapper.Map<OfficeDto>(result));
        }

        [HttpPost]
        public async Task<ActionResult> Create(OfficeCreationDto office)
        {
            if (await officeService.IsOfficeAlreadyCreatedByName(office.Name))
            {
                return BadRequest($"Name {office.Name} is already associated to a Office, please chose other");
            }
            var result = await officeService.CreateOfficeAsync(mapper.Map<Office>(office));

            return Ok(mapper.Map<OfficeDto>(result));
        }
    }
}