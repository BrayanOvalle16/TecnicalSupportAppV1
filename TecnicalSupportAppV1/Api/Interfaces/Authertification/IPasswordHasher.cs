namespace TecnicalSupportAppV1.Api.Interfaces.Authertification
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        bool Verify(string passwordhash, string inputpassword);
    }
}
