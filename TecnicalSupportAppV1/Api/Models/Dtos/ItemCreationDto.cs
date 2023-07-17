namespace TecnicalSupportAppV1.Api.Models.Dtos
{
    public class ItemCreationDto : AbstractEntityBaseDto
    {
        public string Description { get; set; }
        public string UnitPrice { get; set; }
    }
}
