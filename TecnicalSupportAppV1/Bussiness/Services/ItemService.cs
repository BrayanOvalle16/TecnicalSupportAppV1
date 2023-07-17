using TecnicalSupportAppV1.Api.Interfaces.Dao;
using TecnicalSupportAppV1.Api.Interfaces.Services;
using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Bussiness.Services
{
    public class ItemService : IItemService
    {
        private readonly IItemDao _itemDao;

        public ItemService(IItemDao context)
        {
            _itemDao = context;
        }

        public async Task<List<Item>> GetItemListAsync(long officeId)
        {
            return await _itemDao.GetItemListAsync(officeId);
        }

        public async Task<Item> CreateItemAsync(Item item)
        {
            if (item != null)
            {
                await _itemDao.CreateItemAsync(item);
            }
            return item;
        }

        public async Task<Item> UpdateItemAsync(Item item)
        {
            await _itemDao.UpdateItemAsync(item);
            return item;
        }

        public async Task<Item> FindItemById(long id, long officeId)
        {
            return await _itemDao.FindItemById(id, officeId);
        }

        public async Task DeleteItemById(long id, long officeId)
        {
            await _itemDao.DeleteItemById(id, officeId);
        }

        public Task<bool> IsItemAlreadyCreatedByDescription(string description, long officeId)
        {
            return _itemDao.IsItemAlreadyCreatedByDescription(description, officeId);
        }
    }
}
