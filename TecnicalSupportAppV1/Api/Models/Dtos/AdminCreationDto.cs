using System.ComponentModel.DataAnnotations.Schema;
using TecnicalSupportAppV1.Api.Models;

namespace TecnicalSupportAppV1.Api.Models.Dtos
{
    public class AdminCreationDto : AbstractEntityBaseDto
    {
        public long UserId { get; set; }
    }
}
