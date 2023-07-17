using TecnicalSupportAppV1.Api.Interfaces.Dao;
using TecnicalSupportAppV1.Api.Interfaces.Services;
using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Bussiness.Services
{
    public class TechnicianService : ITechnicianService
    {
        private readonly ITechnicianDao _technicianDao;

        public TechnicianService(ITechnicianDao context)
        {
            _technicianDao = context;
        }

        public async Task<List<Technician>> GetTechnicianListAsync(long officeId)
        {
            return await _technicianDao.GetTechnicianListAsync(officeId);
        }

        public async Task<Technician> CreateTechnicianAsync(Technician technician)
        {
            if (technician != null)
            {
                await _technicianDao.CreateTechnicianAsync(technician);
            }
            return technician;
        }

        public async Task<Technician> UpdateTechnicianAsync(Technician technician)
        {
            await _technicianDao.UpdateTechnicianAsync(technician);
            return technician;
        }

        public async Task<Technician> FindTechnicianById(long? id, long officeId)
        {
            if(id == null)
            {
                return null;
            }
            return await _technicianDao.FindTechnicianById(id.Value, officeId);
        }

        public async Task DeleteTechnicianById(long id, long officeId)
        {
            await _technicianDao.DeleteTechnicianById(id, officeId);
        }

        public Task<bool> IsTechnicianAlreadyCreatedByUserId(long userId, long officeId)
        {
            return _technicianDao.IsTechnicianAlreadyCreatedByUserId(userId, officeId);
        }
    }
}
