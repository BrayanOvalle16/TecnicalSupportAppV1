using System.ComponentModel.DataAnnotations.Schema;

namespace TecnicalSupportAppV1.Api.Models.Dtos
{
    public class TechnicianDto : AbstractEntityBaseDto
    {
        public long Id { get; set; }
        public UserDto User { get; set; }
        public OfficeDto Office { get; set; }
        public long UserId { get; set; }
        public List<StockTechnicianDto> Stocks { get; set; }
    }
}
