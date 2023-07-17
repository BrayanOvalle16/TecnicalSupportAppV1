using System.ComponentModel.DataAnnotations.Schema;
using TecnicalSupportAppV1.Api.Models.Enums;

namespace TecnicalSupportAppV1.Api.Models.Dtos
{
    public class ServiceOrderDto : AbstractEntityBaseDto
    {
        public long Id { get; set; }
        public ClientDto Client { get; set; }
        public long ClientId { get; set; }
        public TechnicianDto Technician { get; set; }
        public long TechnicianId { get; set; }
        public string? Description { get; set; }
        public string? ResolutionDescription { get; set; }
        public DateTime? AppointmentStartDate { get; set; }
        public DateTime? AppointmentEndDate { get; set; }
        public OfficeDto Office { get; set; }
        public long OfficeId { get; set; }
        public ServiceStateEnum ServiceState { get; set; }
    }
}
