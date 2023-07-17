using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Api.Interfaces.Services
{
    public interface IServiceOrderService
    {
        Task<List<ServiceOrder>> GetServiceOrderListAsync(long officeId);
        Task<ServiceOrder> CreateServiceOrderAsync(ServiceOrder ServiceOrder);
        Task<ServiceOrder> UpdateServiceOrderAsync(ServiceOrder ServiceOrder);
        Task<ServiceOrder> FindServiceOrderById(long id, long officeId);
        Task DeleteServiceOrderById(long id, long officeId);
        Task<bool> IsServiceOrderAlreadyCreatedByDescription(string description, long officeId);
        Task<bool> IsServiceOrderAlreadyCreatedByDateAndTechnician(long technicianId, DateTime startDate, DateTime endTime);
        Task<ServiceOrder> CloseTicket(long id, long officeId, string resolutionDescription);
    }
}
