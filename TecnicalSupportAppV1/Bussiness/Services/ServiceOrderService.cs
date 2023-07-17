using TecnicalSupportAppV1.Api.Interfaces.Dao;
using TecnicalSupportAppV1.Api.Interfaces.Services;
using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Bussiness.Services
{
    public class ServiceOrderService : IServiceOrderService
    {
        private readonly IServiceOrderDao _serviceOrderDao;

        public ServiceOrderService(IServiceOrderDao context)
        {
            _serviceOrderDao = context;
        }

        public async Task<List<ServiceOrder>> GetServiceOrderListAsync(long officeId)
        {
            return await _serviceOrderDao.GetServiceOrderListAsync(officeId);
        }

        public async Task<ServiceOrder> CreateServiceOrderAsync(ServiceOrder serviceOrder)
        {
            if (serviceOrder != null)
            {
                await _serviceOrderDao.CreateServiceOrderAsync(serviceOrder);
            }
            return serviceOrder;
        }

        public async Task<ServiceOrder> UpdateServiceOrderAsync(ServiceOrder serviceOrder)
        {
            await _serviceOrderDao.UpdateServiceOrderAsync(serviceOrder);
            return serviceOrder;
        }

        public async Task<ServiceOrder> FindServiceOrderById(long id, long officeId)
        {
            return await _serviceOrderDao.FindServiceOrderById(id, officeId);
        }

        public async Task<ServiceOrder> CloseTicket(long id, long officeId, string resolutionDescription )
        {
            ServiceOrder serviceOrder = await _serviceOrderDao.FindServiceOrderById(id, officeId);
            serviceOrder.ResolutionDescription = resolutionDescription;
            serviceOrder.ServiceState = Api.Models.Enums.ServiceStateEnum.Closed;
            return await _serviceOrderDao.UpdateServiceOrderAsync(serviceOrder);
        }

        public async Task DeleteServiceOrderById(long id, long officeId)
        {
            await _serviceOrderDao.DeleteServiceOrderById(id, officeId);
        }

        public Task<bool> IsServiceOrderAlreadyCreatedByDescription(string description, long officeId)
        {
            return _serviceOrderDao.IsServiceOrderAlreadyCreatedByDescription(description, officeId);
        }

        public Task<bool> IsServiceOrderAlreadyCreatedByDateAndTechnician(long technicianId, DateTime startDate, DateTime endTime)
        {
            return _serviceOrderDao.IsServiceOrderAlreadyCreatedByDateAndTechnician(technicianId, startDate, endTime);
        }
    }
}
