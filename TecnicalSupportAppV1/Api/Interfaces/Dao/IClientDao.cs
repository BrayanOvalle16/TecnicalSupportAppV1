using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Api.Interfaces.Dao
{
    public interface IClientDao
    {
        Task<List<Client>> GetClientListAsync();
        Task<Client> CreateClientAsync(Client client);
        Task<Client> UpdateClientAsync(Client client);
        Task<Client> FindClientById(long id);
        Task DeleteClientById(long id);
        Task<bool> IsClientAlreadyCreatedByUserId(long userId);
    }
}
