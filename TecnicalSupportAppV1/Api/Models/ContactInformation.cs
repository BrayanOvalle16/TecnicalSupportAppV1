using System.ComponentModel.DataAnnotations;

namespace TecnicalSupportAppV1.Api.Models
{
    [Index(nameof(IdentificationNumber), IsUnique = true)]
    public class ContactInformation : AbstractEntityBase
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string? IdentificationNumber { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
    }
}
