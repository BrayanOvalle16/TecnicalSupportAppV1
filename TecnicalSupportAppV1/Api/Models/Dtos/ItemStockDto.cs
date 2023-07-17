namespace TecnicalSupportAppV1.Api.Models.Dtos
{
    public class ItemStockDto : AbstractEntityBaseDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string UnitPrice { get; set; }
        public long OfficeId { get; set; }
        public OfficeDto Office { get; set; }
    }
}
