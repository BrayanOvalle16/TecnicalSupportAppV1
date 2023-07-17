using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Api.Interfaces.Services
{
    public interface ITechnicianService
    {
        Task<List<Technician>> GetTechnicianListAsync(long officeId);
        Task<Technician> CreateTechnicianAsync(Technician Technician);
        Task<Technician> UpdateTechnicianAsync(Technician Technician);
        Task<Technician> FindTechnicianById(long? id, long officeId);
        Task DeleteTechnicianById(long id, long officeId);
        Task<bool> IsTechnicianAlreadyCreatedByUserId(long userId, long officeId);
    }
}
