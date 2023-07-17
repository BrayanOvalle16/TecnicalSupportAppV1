using System.ComponentModel.DataAnnotations.Schema;
using TecnicalSupportAppV1.Api.Models.Enums;

namespace TecnicalSupportAppV1.Api.Models.Dtos
{
    public class StockCreationDto 
    {
        public string ExternalItemId { get; set; }
        public long? TechnicianId { get; set; }
        public long ItemId { get; set; }
        public bool IsDamage { get; set; } = false;
        public List<NotesCreationDto> Notes { get; set; }
    }
}
