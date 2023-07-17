using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Api.Interfaces.Facades
{
    public interface IAuthFacade
    {
        Task<bool> VerifyLogin(string password, string userName);
        Task<string> Hash(string password);
        string CreateJwtToken(string username);
    }
}
