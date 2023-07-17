using TecnicalSupportAppV1.Api.Models.Enums;

namespace TecnicalSupportAppV1.Api.Models.Dtos
{
    public class StockUpdateDto : AbstractEntityBaseDto
    {
        public long Id { get; set; }
        public string ExternalItemId { get; set; }
        public long TechnicianId { get; set; }
        public long ItemId { get; set; }
        public ICollection<Notes>? Notes { get; set; }
        public bool IsDamage { get; set; }
    }
}
