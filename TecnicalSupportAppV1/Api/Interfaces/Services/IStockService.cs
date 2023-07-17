using TecnicalSupportAppV1.Api.Models;
using TecnicalSupportAppV1.Api.Models.Enums;

namespace TecnicalSupportAppV1.Api.Interfaces.Services
{
    public interface IStockService
    {
        Task<List<Stock>> GetStockListAsync(long officeId);
        Task<Stock> CreateStockAsync(Stock Stock);
        Task<Stock> UpdateStockAsync(Stock Stock);
        Task<Stock> FindStockById(long id, long officeId);
        Task DeleteStockById(long id, long officeId);
        Task<bool> IsStockAlreadyAssignByItemId(string itemId, long officeId);
        StockAvailability GetStockAvailability(long? itemId, bool isDamage);
        Task<bool> IsStockAlreadyAssignByItemIdAndOffice(string itemId, long officeId);
    }
}
