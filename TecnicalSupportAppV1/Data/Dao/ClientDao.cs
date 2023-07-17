using Microsoft.EntityFrameworkCore;
using TecnicalSupportAppV1.Api.Interfaces.Dao;
using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Data.Dao
{
    public class ClientDao : IClientDao
    {
        private readonly DataContext _context;

        public ClientDao(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Client>> GetClientListAsync()
        {
            return await _context.Clients
                .Include(x => x.User)
                .ThenInclude(x => x.ContactInformation)
                .ToListAsync();
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client> UpdateClientAsync(Client client)
        {
            _context.Update(client);
            _context.Entry(client).Property(x => x.User).IsModified = false;
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client> FindClientById(long id)
        {
            Client client = await _context.Clients
                .Include(x => x.User)
                .ThenInclude(x => x.ContactInformation)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
            return client;
        }

        public async Task DeleteClientById(long id)
        {
            Client client = await FindClientById(id);
            _context.Remove(client);
            await _context.SaveChangesAsync();
        }

        public Task<bool> IsClientAlreadyCreatedByUserId(long userId)
        {
            return _context.Clients
                .AnyAsync(x => x.UserId == userId);
        }
    }
}
