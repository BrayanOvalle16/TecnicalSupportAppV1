using System.ComponentModel.DataAnnotations.Schema;
using TecnicalSupportAppV1.Api.Models.Enums;

namespace TecnicalSupportAppV1.Api.Models
{
    public class ServiceOrder : AbstractEntityBase
    {
        public long Id { get; set; }
        public Client Client { get; set; }
        [ForeignKey(nameof(Client))]
        public long ClientId { get; set; }
        public Technician Technician { get; set; }
        [ForeignKey(nameof(Technician))]
        public long TechnicianId { get; set; }
        public string? Description { get; set; }
        public string? ResolutionDescription { get; set; }
        public DateTime? AppointmentStartDate { get; set; }
        public DateTime? AppointmentEndDate { get; set; }
        public Office Office { get; set; }
        [ForeignKey(nameof(Office))]
        public long OfficeId { get; set; }
        public ServiceStateEnum ServiceState { get; set; }
    }
}
