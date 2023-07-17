using Azure.Core;
using TecnicalSupportAppV1.Api.Interfaces.Dao;
using TecnicalSupportAppV1.Api.Interfaces.Services;
using TecnicalSupportAppV1.Api.Models;
using TecnicalSupportAppV1.Api.Models.Enums;

namespace TecnicalSupportAppV1.Bussiness.Services
{
    public class StockService : IStockService
    {
        private readonly IStockDao _stockDao;

        public StockService(IStockDao context)
        {
            _stockDao = context;
        }

        public async Task<List<Stock>> GetStockListAsync(long officeId)
        {
            return await _stockDao.GetStockListAsync(officeId);
        }

        public async Task<Stock> CreateStockAsync(Stock stock)
        {
            if (stock != null)
            {
                await _stockDao.CreateStockAsync(stock);
            }
            return stock;
        }

        public async Task<Stock> UpdateStockAsync(Stock stock)
        {
            await _stockDao.UpdateStockAsync(stock);
            return stock;
        }

        public StockAvailability GetStockAvailability(long? itemId, bool isDamage)
        {
            if (isDamage)
            {
                return StockAvailability.Damage;
            }
            else if (itemId == null)
            {
                return StockAvailability.OnInvetory;
            }

            return StockAvailability.AssignedToTechnician;
        }

        public async Task<Stock> FindStockById(long id, long officeId)
        {
            return await _stockDao.FindStockById(id, officeId);
        }

        public async Task DeleteStockById(long id, long officeId)
        {
            await _stockDao.DeleteStockById(id, officeId);
        }

        public Task<bool> IsStockAlreadyAssignByItemId(string itemId, long officeId)
        {
            return _stockDao.IsStockAlreadyAssignByItemId(itemId, officeId, StockAvailability.AssignedToTechnician);
        }
        public Task<bool> IsStockAlreadyAssignByItemIdAndOffice(string itemId, long officeId)
        {
            return _stockDao.IsStockAlreadyAssignByItemId(itemId, officeId);
        }
    }
}