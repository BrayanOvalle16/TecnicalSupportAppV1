namespace TecnicalSupportAppV1.Api.Models.Dtos
{
    public class ItemUpdateDto : AbstractEntityBaseDto
    {
        public long Id { get; set; }
        public string Description { get; set; }
        public string UnitPrice { get; set; }
    }
}
