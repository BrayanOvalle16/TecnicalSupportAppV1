using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Api.Models.Dtos
{
    public class UserCreationDto : AbstractEntityBaseDto
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public ContactInformationCreationDto? ContactInformation { get; set; }
    }
}
