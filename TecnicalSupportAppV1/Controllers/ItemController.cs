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
    [Route("api/service/office/{officeId}/items")]
    [Authorize(Roles = "SuperAdmin,Admin")]
    public class ItemController : ControllerBase
    {
        private readonly IItemService itemService;
        private readonly IMapper mapper;

        public ItemController(IItemService _itemService, IMapper _mapper)
        {
            mapper = _mapper;
            itemService = _itemService;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll(long officeId)
        {
            List<Item> itemList = await itemService.GetItemListAsync(officeId);
            return Ok(mapper.Map<List<ItemDto>>(itemList));
        }

        [HttpGet("find-by-id")]
        public async Task<ActionResult> GetById(long id, long officeId)
        {
            Item itemList = await itemService.FindItemById(id, officeId);
            if (itemList == null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<ItemDto>(itemList));
        }

        [HttpDelete()]
        public async Task<ActionResult> DeleteById(long id, long officeId)
        {
            await itemService.DeleteItemById(id, officeId);
            return Ok();
        }

        [HttpPut()]
        public async Task<ActionResult> UpdateById(ItemUpdateDto item, long officeId)
        {
            string description = item.Description;
            if (description == null)
            {
                return BadRequest("Please assign a description to the item");
            }

            var itemOnDb = await itemService.FindItemById(item.Id, officeId);
            if (itemOnDb.Description != description && await itemService.IsItemAlreadyCreatedByDescription(description, officeId))
            {
                return BadRequest($"Item already associated with description:  {description}");
            }
            var request = mapper.Map<Item>(item);
            request.OfficeId = officeId;
            var result = await itemService.UpdateItemAsync(request);

            return Ok(mapper.Map<ItemDto>(result));
        }

        [HttpPost]
        public async Task<ActionResult> Create(ItemCreationDto item, long officeId)
        {
            string description = item.Description;
            if (description == null)
            {
                return BadRequest("Please assign a description to the item");
            }

            if (await itemService.IsItemAlreadyCreatedByDescription(description, officeId))
            {
                return BadRequest($"Item already associated with description:  {description}");
            }
            var request = mapper.Map<Item>(item);
            request.OfficeId = officeId;
            var result = await itemService.CreateItemAsync(request);

            return Ok(mapper.Map<ItemDto>(result));
        }

/*        [HttpPost]
        public async Task<ActionResult> AssignItemTo(ItemCreationDto item, long officeId)
        {
            string description = item.Description;
            if (description == null)
            {
                return BadRequest("Please assign a description to the item");
            }

            if (await itemService.IsItemAlreadyCreatedByDescription(description, officeId))
            {
                return BadRequest($"Item already associated with description:  {description}");
            }
            var request = mapper.Map<Item>(item);
            request.OfficeId = officeId;
            var result = await itemService.CreateItemAsync(request);

            return Ok(mapper.Map<ItemDto>(result));
        }
*/    }
}
