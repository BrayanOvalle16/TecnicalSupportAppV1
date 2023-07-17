namespace TecnicalSupportAppV1.Api.Models.Dtos
{
    public class AbstractEntityBaseDto
    {
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}
