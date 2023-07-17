namespace TecnicalSupportAppV1.Api.Models
{
    public abstract class AbstractEntityBase
    {
        public bool IsActive { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
    }
}
