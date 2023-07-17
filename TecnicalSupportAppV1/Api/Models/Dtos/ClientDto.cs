using System.ComponentModel.DataAnnotations.Schema;

namespace TecnicalSupportAppV1.Api.Models.Dtos
{
    public class ClientDto : AbstractEntityBaseDto
    {
        public int Id { get; set; }
        public UserDto User { get; set; }
        public long? UserId { get; set; }
    }
}
