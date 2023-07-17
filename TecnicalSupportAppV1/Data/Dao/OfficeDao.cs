using TecnicalSupportAppV1.Api.Interfaces.Dao;
using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Data.Dao
{
    public class OfficeDao : IOfficeDao
    {
        private readonly DataContext _context;

        public OfficeDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Office>> GetOfficeListAsync()
        {
            return await _context.Offices
                .ToListAsync();
        }

        public async Task<Office> CreateOfficeAsync(Office office)
        {
            _context.Offices.Add(office);
            await _context.SaveChangesAsync();
            return office;
        }

        public async Task<Office> UpdateOfficeAsync(Office office)
        {
            _context.Update(office);
            await _context.SaveChangesAsync();
            return office;
        }

        public async Task<Office> FindOfficeById(long id)
        {
            Office office = await _context.Offices
                .Where(x => x.Id == id)
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return office;
        }

        public async Task DeleteOfficeById(long id)
        {
            Office office = await FindOfficeById(id);
            _context.Remove(office);
            await _context.SaveChangesAsync();
        }

        public Task<bool> IsOfficeAlreadyCreatedByName(string name)
        {
            return _context.Offices
                .AnyAsync(x => x.Name.Equals(name));
        }
    }
}