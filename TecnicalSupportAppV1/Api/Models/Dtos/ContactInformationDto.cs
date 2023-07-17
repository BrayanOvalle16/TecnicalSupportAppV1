using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Api.Models.Dtos
{
    public class ContactInformationDto : AbstractEntityBaseDto
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
    }
}
