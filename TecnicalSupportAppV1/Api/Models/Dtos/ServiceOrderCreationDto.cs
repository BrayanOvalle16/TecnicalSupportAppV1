using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecnicalSupportAppV1.Api.Models.Dtos
{
    public class ServiceOrderCreationDto
    {
        [Required]
        public long ClientId { get; set; }
        [Required]
        public long TechnicianId { get; set; }
        public string? Description { get; set; }
        public DateTime? AppointmentStartDate { get; set; }
        public DateTime? AppointmentEndDate { get; set; }
    }
}
