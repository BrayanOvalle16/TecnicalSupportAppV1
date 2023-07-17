using TecnicalSupportAppV1.Api.Interfaces.Dao;
using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Data.Dao
{
    public class ItemDao : IItemDao
    {
        private readonly DataContext _context;

        public ItemDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Item>> GetItemListAsync(long officeId)
        {
            return await _context.Items
                .Include(x => x.Office)
                .Include(x => x.Stocks)
                .Where(x => officeId.Equals(x.OfficeId))
                .ToListAsync();
        }

        public async Task<Item> CreateItemAsync(Item admin)
        {
            _context.Items.Add(admin);
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<Item> UpdateItemAsync(Item admin)
        {
            _context.Update(admin);
            _context.Entry(admin).Property(x => x.Office).IsModified = false;
            await _context.SaveChangesAsync();
            return admin;
        }

        public async Task<Item> FindItemById(long id, long officeId)
        {
            Item admin = await _context.Items
                .Include(x => x.Office)
                .Include(x => x.Stocks)
                .Where(x => x.Id == id && officeId.Equals(x.OfficeId))
                .AsNoTracking()
                .FirstOrDefaultAsync();
            return admin;
        }

        public async Task DeleteItemById(long id, long officeId)
        {
            Item admin = await FindItemById(id, officeId);
            _context.Remove(admin);
            await _context.SaveChangesAsync();
        }

        public Task<bool> IsItemAlreadyCreatedByDescription(string description, long officeId)
        {
            return _context.Items
                .AnyAsync(x => description == x.Description && officeId == x.OfficeId);
        }
    }
}