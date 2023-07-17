using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Api.Models.Dtos
{
    public class UserDto : AbstractEntityBaseDto
    {
        public long Id { get; set; }
        public string Username { get; set; }
        public ContactInformationDto? ContactInformation { get; set; }
    }
}
