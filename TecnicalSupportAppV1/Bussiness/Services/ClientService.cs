using TecnicalSupportAppV1.Api.Interfaces.Dao;
using TecnicalSupportAppV1.Api.Interfaces.Services;
using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Bussiness.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientDao _clientDao;

        public ClientService(IClientDao context)
        {
            _clientDao = context;
        }

        public async Task<List<Client>> GetClientListAsync()
        {
            return await _clientDao.GetClientListAsync();
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            if (client != null)
            {
                await _clientDao.CreateClientAsync(client);
            }
            return client;
        }

        public async Task<Client> UpdateClientAsync(Client client)
        {
            await _clientDao.UpdateClientAsync(client);
            return client;
        }

        public async Task<Client> FindClientById(long id)
        {
            return await _clientDao.FindClientById(id);
        }

        public async Task DeleteClientById(long id)
        {
            await _clientDao.DeleteClientById(id);
        }

        public Task<bool> IsClientAlreadyCreatedByUserId(long userId)
        {
            return _clientDao.IsClientAlreadyCreatedByUserId(userId);
        }
    }
}