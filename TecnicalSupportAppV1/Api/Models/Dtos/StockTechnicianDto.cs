using TecnicalSupportAppV1.Api.Models.Enums;

namespace TecnicalSupportAppV1.Api.Models.Dtos
{
    public class StockTechnicianDto : AbstractEntityBaseDto
    {
        public long Id { get; set; }
        public string ExternalItemId { get; set; }
        public long TechnicianId { get; set; }
        public ItemStockDto Item { get; set; }
        public long ItemId { get; set; }
        public List<NotesCreationDto>? Notes { get; set; }
        public StockAvailability? StockAvailability { get; set; }
        public OfficeDto Office { get; set; }
        public long OfficeId { get; set; }
    }
}
