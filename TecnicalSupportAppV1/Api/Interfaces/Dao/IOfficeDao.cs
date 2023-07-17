using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Api.Interfaces.Dao
{
    public interface IOfficeDao
    {
        Task<List<Office>> GetOfficeListAsync();
        Task<Office> CreateOfficeAsync(Office office);
        Task<Office> UpdateOfficeAsync(Office office);
        Task<Office> FindOfficeById(long id);
        Task DeleteOfficeById(long id);
        Task<bool> IsOfficeAlreadyCreatedByName(string name);
    }
}
