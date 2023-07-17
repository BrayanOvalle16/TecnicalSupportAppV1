using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using TecnicalSupportAppV1.Api.Interfaces.Services;
using TecnicalSupportAppV1.Api.Models.Dtos;
using TecnicalSupportAppV1.Api.Models;
using TecnicalSupportAppV1.Bussiness.Services;
using TecnicalSupportAppV1.Api.Models.Enums;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authorization;

namespace TecnicalSupportAppV1.Controllers
{
    [ApiController]
    [Route("api/service/office/{officeId}/stock")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class StockController : ControllerBase
    {
        private readonly IStockService stockService;
        private readonly IItemService itemService;
        private readonly ITechnicianService technicianService;
        private readonly IMapper mapper;

        public StockController(IStockService _stockService, IMapper _mapper, IItemService _itemService, ITechnicianService _technicianService)
        {
            mapper = _mapper;
            stockService = _stockService;
            itemService = _itemService;
            technicianService = _technicianService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(long officeId)
        {
            List<Stock> stockList = await stockService.GetStockListAsync(officeId);
            return Ok(mapper.Map<List<StockDto>>(stockList));
        }

        [HttpGet("find-by-id")]
        public async Task<ActionResult> GetById(long id, long officeId)
        {
            Stock stockList = await stockService.FindStockById(id, officeId);
            if (stockList == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<StockDto>(stockList));
        }

        [HttpDelete()]
        public async Task<ActionResult> DeleteById(long id, long officeId)
        {
            Stock admin = await stockService.FindStockById(id, officeId);
            if(admin == null)
            {
                return NotFound();
            }
            await stockService.DeleteStockById(id, officeId);
            return Ok();
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateById(StockUpdateDto stock, long officeId)
        {
            var stockId = stock.ItemId;
            if (stock.Id == 0)
            {
                return NotFound();
            }

            if (stockId == 0)
            {
                return BadRequest("Please assign a stockId to the stock assignment.");
            }

            Technician technician = await technicianService.FindTechnicianById(stock.TechnicianId, officeId);
            if (stock.TechnicianId != 0 && technician == null)
            {
                return BadRequest("Technician not found.");
            }

            Item item = await itemService.FindItemById(stockId, officeId);
            if (item == null)
            {
                return BadRequest("Item not found.");
            }

            var stockOnDb = await stockService.FindStockById(stock.Id, officeId);
            if (!stock.ExternalItemId.IsNullOrEmpty() 
                && stockOnDb.ExternalItemId != stock.ExternalItemId  
                && await stockService.IsStockAlreadyAssignByItemIdAndOffice(stock.ExternalItemId, officeId))
            {
                return BadRequest($"Item with id {stock.ExternalItemId} is already created.");
            }

            if (stock.TechnicianId != 0 &&
                stockOnDb.TechnicianId != stock.TechnicianId &&
                await stockService.IsStockAlreadyAssignByItemId(stock.ExternalItemId, officeId) )
            {
                return BadRequest($"Item with id {stockId} is already assign to another technician.");
            }
            var request = mapper.Map<Stock>(stock);
            request.OfficeId = officeId;
            request.Technician = technician;
            request.StockAvailability = stockService.GetStockAvailability(stock.TechnicianId, stock.IsDamage);
            if (request.StockAvailability == StockAvailability.Damage)
            {
                request.Technician = null;
                request.TechnicianId = null;
            }
            var result = await stockService.UpdateStockAsync(request);

            return Ok(mapper.Map<StockDto>(result));
        }

        [HttpPost]
        public async Task<ActionResult> Create(StockCreationDto stock, long officeId)
        {
            var stockId = stock.ItemId;
            if (stockId == 0)
            {
                return BadRequest("Please assign a stockId to the stock assignment.");
            }

            Technician technician = await technicianService.FindTechnicianById(stock.TechnicianId, officeId);
            if (stock.TechnicianId != null && technician == null)
            {
                return BadRequest("Technician not found.");
            }

            Item item = await itemService.FindItemById(stockId, officeId);
            if (item == null)
            {
                return BadRequest("Item not found.");
            }

            if (!stock.ExternalItemId.IsNullOrEmpty() && await stockService.IsStockAlreadyAssignByItemIdAndOffice(stock.ExternalItemId, officeId))
            {
                return BadRequest($"Item with id {stock.ExternalItemId} is already created.");
            }

            if (!stock.ExternalItemId.IsNullOrEmpty() && await stockService.IsStockAlreadyAssignByItemId(stock.ExternalItemId, officeId))
            {
                return BadRequest($"Item with id {stock.ExternalItemId} is already assign to another technician.");
            }

            var request = mapper.Map<Stock>(stock);
            request.OfficeId = officeId;
            request.StockAvailability = stockService.GetStockAvailability(stock.TechnicianId, stock.IsDamage);
            if (request.StockAvailability == StockAvailability.Damage)
            {
                request.Technician = null;
                request.TechnicianId = null;
            }
            var result = await stockService.CreateStockAsync(request);

            return Ok(mapper.Map<StockDto>(result));
        }
    }
}
