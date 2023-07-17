namespace TecnicalSupportAppV1.Api.Models.Dtos
{
    public class ServiceOrderUpdateDto : AbstractEntityBaseDto
    {
        public long Id { get; set; }
        public long ClientId { get; set; }
        public long TechnicianId { get; set; }
        public string? Description { get; set; }
        public DateTime? AppointmentStartDate { get; set; }
        public DateTime? AppointmentEndDate { get; set; }
        public long OfficeId { get; set; }
    }
}
