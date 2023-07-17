using System.ComponentModel.DataAnnotations.Schema;

namespace TecnicalSupportAppV1.Api.Models.Dtos
{
    public class TechnicianCreationDto 
    {
        public long UserId { get; set; }
        public long OfficeId;
    }
}
