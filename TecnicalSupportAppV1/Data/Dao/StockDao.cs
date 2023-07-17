using TecnicalSupportAppV1.Api.Interfaces.Dao;
using TecnicalSupportAppV1.Api.Models;
using TecnicalSupportAppV1.Api.Models.Enums;

namespace TecnicalSupportAppV1.Data.Dao
{
    public class StockDao : IStockDao
    {
        private readonly DataContext _context;

        public StockDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Stock>> GetStockListAsync(long officeId)
        {
            return await _context.Stocks
                .Include(x => x.Notes)
                .Include(x => x.Technician)
                .ThenInclude(x => x.User)
                .ThenInclude(x => x.ContactInformation)
                .Include(x => x.Item)
                .Include(x => x.Office)
                .Where(x => officeId.Equals(x.OfficeId))
                .ToListAsync();
        }

        public async Task<Stock> CreateStockAsync(Stock admin)
        {
            _context.Stocks.Add(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<Stock> UpdateStockAsync(Stock admin)
        {
            _context.Update(admin);
            _context.Entry(admin).Reference(x => x.Office).IsModified = false;
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<Stock> FindStockById(long id, long officeId)
        {
            Stock admin = await _context.Stocks
                .Include(x => x.Notes)
                .Include(x => x.Technician)
                .ThenInclude(x => x.User)
                .ThenInclude(x => x.ContactInformation)
                .Include(x => x.Item)
                .Include(x => x.Office)
                .Where(x => x.Id == id && officeId.Equals(x.OfficeId))
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return admin;
        }

        public async Task DeleteStockById(long id, long officeId)
        {
            Stock admin = await FindStockById(id, officeId);
            if(admin != null)
            {
                if(admin.Notes != null)
                {
                    _context.RemoveRange(admin.Notes);
                }
                _context.Remove(admin);
                await _context.SaveChangesAsync();
            }
        }

        public Task<bool> IsStockAlreadyAssignByItemId(string itemId, long officeId, StockAvailability availability)
        {
            return _context.Stocks
                .AnyAsync(x => itemId == x.ExternalItemId 
                && officeId == x.OfficeId 
                && x.StockAvailability == availability);
        }

        public Task<bool> IsStockAlreadyAssignByItemId(string itemId, long officeId)
        {
            return _context.Stocks
                .AnyAsync(x => itemId == x.ExternalItemId
                && officeId == x.OfficeId);
        }
    }
}