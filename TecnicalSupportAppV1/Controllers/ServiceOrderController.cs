using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TecnicalSupportAppV1.Api.Interfaces.Services;
using TecnicalSupportAppV1.Api.Models.Dtos;
using TecnicalSupportAppV1.Api.Models;
using TecnicalSupportAppV1.Api.Models.Enums;
using Microsoft.AspNetCore.Authorization;

namespace TecnicalSupportAppV1.Controllers
{
    [ApiController]
    [Route("api/service/office/{officeId}/service-order")]
    public class ServiceOrderController : ControllerBase
    {
        private readonly IServiceOrderService serviceOrderService;
        private readonly IClientService clientService;
        private readonly ITechnicianService technicianService;
        private readonly IOfficeService officeService;
        private readonly IMapper mapper;

        public ServiceOrderController(IServiceOrderService _serviceOrderService, IMapper _mapper,
            IClientService _clientService, ITechnicianService _technicianService, IOfficeService _officeService)
        {
            mapper = _mapper;
            clientService = _clientService;
            officeService = _officeService;
            technicianService = _technicianService;
            serviceOrderService = _serviceOrderService;
        }

        [HttpGet]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<ActionResult> GetAll(long officeId)
        {
            List<ServiceOrder> serviceOrderList = await serviceOrderService.GetServiceOrderListAsync(officeId);
            return Ok(mapper.Map<List<ServiceOrderDto>>(serviceOrderList));
        }

        [HttpGet("find-by-id")]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<ActionResult> GetById(long id, long officeId)
        {
            ServiceOrder serviceOrderList = await serviceOrderService.FindServiceOrderById(id, officeId);
            if (serviceOrderList == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ServiceOrderDto>(serviceOrderList));
        }

        [HttpDelete()]
        public async Task<ActionResult> DeleteById(long id, long officeId)
        {
            await serviceOrderService.DeleteServiceOrderById(id, officeId);
            return Ok();
        }

        [HttpPut()]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<ActionResult> UpdateById(ServiceOrderUpdateDto serviceOrder, long officeId)
        {
            var serviceOrderDto = await serviceOrderService.FindServiceOrderById(serviceOrder.Id, officeId);
            if(serviceOrderDto == null)
            {
                return NotFound();
            }
            long clientId = serviceOrder.ClientId;
            if (clientId == 0 ||  serviceOrderDto.ClientId != clientId)
            {
                return BadRequest("Client id not set");
            }

            var client = await clientService.FindClientById(clientId);
            if (client == null)
            {
                return NotFound($"Client with id {clientId} is not found");
            }

            long technicianId = serviceOrder.TechnicianId;
            if (technicianId == 0 || serviceOrderDto.ClientId != clientId)
            {
                return BadRequest($"Tecnician id not set.");
            }

            var technician = await technicianService.FindTechnicianById(technicianId, officeId);
            if (technician == null)
            {
                return NotFound($"Technician with id {technicianId} is not found");
            }

            var office = await officeService.FindOfficeById(officeId);
            if (office == null)
            {
                return NotFound($"Technician with id {officeId} is not found");
            }
            if (serviceOrder.AppointmentStartDate == null || serviceOrder.AppointmentEndDate == null)
            {
                return BadRequest($"Please specify appointment starting and ending time.");
            }

            var startDate = serviceOrder.AppointmentStartDate.Value;
            var endDate = serviceOrder.AppointmentEndDate.Value;
            if ((startDate < DateTime.UtcNow || endDate < DateTime.UtcNow) || (serviceOrderDto.AppointmentStartDate != startDate && serviceOrderDto.AppointmentEndDate != endDate &&
                await serviceOrderService.IsServiceOrderAlreadyCreatedByDateAndTechnician(technicianId, serviceOrder.AppointmentStartDate.Value,
                serviceOrder.AppointmentEndDate.Value)))
            {
                return BadRequest($"Tecnician is not Available at that hour. Please change the date");
            }

            var request = mapper.Map<ServiceOrder>(serviceOrder);
            request.OfficeId = officeId;
            request.ServiceState = ServiceStateEnum.Active;
            var result = await serviceOrderService.UpdateServiceOrderAsync(request);

            return Ok(mapper.Map<ServiceOrderDto>(result));
        }

        [HttpPost()]
        [Authorize(Roles = "SuperAdmin,Admin")]
        public async Task<ActionResult> Create(ServiceOrderCreationDto serviceOrder, long officeId)
        {
            long clientId = serviceOrder.ClientId;
            if (clientId == 0 )
            {
                return BadRequest("Client id not set");
            }

            var client = await clientService.FindClientById(clientId);
            if (client == null)
            {
                return NotFound($"Client with id {clientId} is not found");
            }

            long technicianId = serviceOrder.TechnicianId;
            if (technicianId == 0)
            {
                return BadRequest($"Tecnician id not set.");
            }

            var technician = await technicianService.FindTechnicianById(technicianId, officeId);
            if (technician == null)
            {
                return NotFound($"Technician with id {technicianId} is not found");
            }

            var office = await officeService.FindOfficeById(officeId);
            if (office == null)
            {
                return NotFound($"Technician with id {officeId} is not found");
            }
            if (serviceOrder.AppointmentStartDate == null || serviceOrder.AppointmentEndDate == null)
            {
                return BadRequest($"Please specify appointment starting and ending time.");
            }

            var startDate = serviceOrder.AppointmentStartDate.Value;
            var endDate = serviceOrder.AppointmentEndDate.Value;
            if ((startDate < DateTime.UtcNow || endDate < DateTime.UtcNow) || (
                await serviceOrderService.IsServiceOrderAlreadyCreatedByDateAndTechnician(technicianId, serviceOrder.AppointmentStartDate.Value,
                serviceOrder.AppointmentEndDate.Value)))
            {
                return BadRequest($"Tecnician is not Available at that hour. Please change the date");
            }

            var request = mapper.Map<ServiceOrder>(serviceOrder);
            request.OfficeId = officeId;
            request.ServiceState = ServiceStateEnum.Active;
            var result = await serviceOrderService.UpdateServiceOrderAsync(request);

            return Ok(mapper.Map<ServiceOrderDto>(result));
        }

        [HttpPost("order-id/{id}/close-order")]
        [Authorize(Roles = "SuperAdmin,Admin,Technician")]
        public async Task<ActionResult> CloseTicket(ServiceOrderClosingDto serviceOrder, long officeId, long id)
        {
            var serviceOrderDto = await serviceOrderService.FindServiceOrderById(id, officeId);
            if (serviceOrderDto == null)
            {
                return NotFound();
            }

            var result = await serviceOrderService.CloseTicket(id, officeId, serviceOrder.ResolutionDescription);

            return Ok(mapper.Map<ServiceOrderDto>(result));
        }

    }
}
