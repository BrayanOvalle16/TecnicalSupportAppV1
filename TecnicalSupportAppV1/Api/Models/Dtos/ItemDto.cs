using System.ComponentModel.DataAnnotations.Schema;

namespace TecnicalSupportAppV1.Api.Models.Dtos
{
    public class ItemDto : AbstractEntityBaseDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string UnitPrice { get; set; }
        public long OfficeId { get; set; }
        public OfficeDto Office { get; set; }
        public List<Stock>? Stocks { get; set; }
    }
}
