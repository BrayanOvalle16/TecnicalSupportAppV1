using TecnicalSupportAppV1.Api.Interfaces.Dao;
using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Data.Dao
{
    public class TechnicianDao : ITechnicianDao
    {
        private readonly DataContext _context;

        public TechnicianDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Technician>> GetTechnicianListAsync(long officeId)
        {
            return await _context.Technicians
                .Include(x => x.Stocks)
                .ThenInclude(x => x.Item)
                .Include(x => x.User)
                .ThenInclude(x => x.ContactInformation)
                .Include(x => x.Office)
                .Where(x => officeId.Equals(x.OfficeId))
                .ToListAsync();
        }

        public async Task<Technician> CreateTechnicianAsync(Technician admin)
        {
            _context.Technicians.Add(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<Technician> UpdateTechnicianAsync(Technician admin)
        {
            _context.Update(admin);
            _context.Entry(admin).Reference(x => x.User).IsModified = false;
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<Technician> FindTechnicianById(long id, long officeId)
        {
            Technician admin = await _context.Technicians
                .Include(x => x.Stocks)
                .ThenInclude(x => x.Item)
                .Include(x => x.User)
                .ThenInclude(x => x.ContactInformation)
                .Include(x => x.Office)
                .Where(x => x.Id == id && officeId.Equals(x.OfficeId))
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return admin;
        }

        public async Task DeleteTechnicianById(long id, long officeId)
        {
            Technician admin = await FindTechnicianById(id, officeId);
            _context.Remove(admin);
            await _context.SaveChangesAsync();
        }

        public Task<bool> IsTechnicianAlreadyCreatedByUserId(long userId, long officeId)
        {
            return _context.Technicians
                .AnyAsync(x => x.UserId == userId && officeId == x.OfficeId);
        }
    }
}