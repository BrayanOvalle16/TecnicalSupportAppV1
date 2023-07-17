using TecnicalSupportAppV1.Api.Interfaces.Dao;
using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Data.Dao
{
    public class ServiceOrderDao : IServiceOrderDao
    {
        private readonly DataContext _context;

        public ServiceOrderDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<ServiceOrder>> GetServiceOrderListAsync(long officeId)
        {
            return await _context.ServiceOrders
                .Include(x => x.Office)
                .Include(x => x.Client)
                .Include(x => x.Technician)
                .Where(x => officeId.Equals(x.OfficeId))
                .ToListAsync();
        }

        public async Task<ServiceOrder> CreateServiceOrderAsync(ServiceOrder admin)
        {
            _context.ServiceOrders.Add(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<ServiceOrder> UpdateServiceOrderAsync(ServiceOrder admin)
        {
            _context.Update(admin);
            _context.Entry(admin).Reference(x => x.Office).IsModified = false;
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<ServiceOrder> FindServiceOrderById(long id, long officeId)
        {
            ServiceOrder admin = await _context.ServiceOrders
                .Include(x => x.Office)
                .Include(x => x.Client)
                .Include(x => x.Technician)
                .Where(x => x.Id == id && officeId.Equals(x.OfficeId))
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return admin;
        }

        public async Task DeleteServiceOrderById(long id, long officeId)
        {
            ServiceOrder admin = await FindServiceOrderById(id, officeId);
            _context.Remove(admin);
            await _context.SaveChangesAsync();
        }

        public Task<bool> IsServiceOrderAlreadyCreatedByDescription(string description, long officeId)
        {
            return _context.ServiceOrders
                .AnyAsync(x => description == x.Description && officeId == x.OfficeId);
        }

        public async Task<bool> IsServiceOrderAlreadyCreatedByDateAndTechnician(long technicianId, DateTime startDate, DateTime endTime)
        {
            return await _context.ServiceOrders
                .Where(x => (x.AppointmentStartDate <= startDate && startDate <= x.AppointmentEndDate ) ||
                (x.AppointmentStartDate <= endTime && endTime <= x.AppointmentEndDate) || 
                (startDate  <= x.AppointmentStartDate && x.AppointmentStartDate  <= endTime) ||
                (startDate <= x.AppointmentEndDate && x.AppointmentEndDate <= endTime) )
                .AnyAsync();
        }
    }
}