using TecnicalSupportAppV1.Api.Interfaces.Dao;
using TecnicalSupportAppV1.Api.Interfaces.Services;
using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Bussiness.Services
{
    public class OfficeService : IOfficeService
    {
        private readonly IOfficeDao _officeDao;

        public OfficeService(IOfficeDao context)
        {
            _officeDao = context;
        }

        public async Task<List<Office>> GetOfficeListAsync()
        {
            return await _officeDao.GetOfficeListAsync();
        }

        public async Task<Office> CreateOfficeAsync(Office office)
        {
            if (office != null)
            {
                await _officeDao.CreateOfficeAsync(office);
            }
            return office;
        }

        public async Task<Office> UpdateOfficeAsync(Office office)
        {
            await _officeDao.UpdateOfficeAsync(office);
            return office;
        }

        public async Task<Office> FindOfficeById(long id)
        {
            return await _officeDao.FindOfficeById(id);
        }

        public async Task DeleteOfficeById(long id)
        {
            await _officeDao.DeleteOfficeById(id);
        }

        public Task<bool> IsOfficeAlreadyCreatedByName(string name)
        {
            return _officeDao.IsOfficeAlreadyCreatedByName(name);
        }
    }
}