using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Api.Interfaces.Services
{
    public interface IItemService
    {
        Task<List<Item>> GetItemListAsync(long officeId);
        Task<Item> CreateItemAsync(Item Item);
        Task<Item> UpdateItemAsync(Item Item);
        Task<Item> FindItemById(long id, long officeId);
        Task DeleteItemById(long id, long officeId);
        Task<bool> IsItemAlreadyCreatedByDescription(string description, long officeId);
    }
}
