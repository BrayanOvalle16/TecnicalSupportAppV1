
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.CompilerServices;

namespace TecnicalSupportAppV1.Api.Models
{
    public class Item : AbstractEntityBase
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string? UnitPrice { get; set; }
        [ForeignKey(nameof(Office))]
        public long OfficeId { get; set; }
        public Office Office { get; set; }
        public List<Stock> Stocks { get; set; }
    }
}
