using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Api.Models.Dtos
{
    public class ContactInformationCreationDto : AbstractEntityBaseDto
    {
        public string Name { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
    }
}
