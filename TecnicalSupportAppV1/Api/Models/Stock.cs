using System.ComponentModel.DataAnnotations.Schema;
using TecnicalSupportAppV1.Api.Models.Enums;

namespace TecnicalSupportAppV1.Api.Models
{
    public class Stock : AbstractEntityBase
    {
        public long Id { get; set; }
        public string ExternalItemId { get; set; }
        public Technician? Technician { get; set; }
        [ForeignKey(nameof(Technician))]
        public long? TechnicianId { get; set; }
        public Item Item { get; set; }
        [ForeignKey(nameof(Item))]
        public long ItemId { get; set; }
        public List<Notes>? Notes { get; set; }
        public StockAvailability? StockAvailability { get; set; }
        public Office Office { get; set; }
        [ForeignKey(nameof(Office))]
        public long OfficeId { get; set; }
    }
}
