using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecnicalSupportAppV1.Api.Models
{
    public class User : AbstractEntityBase
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public List<Roles> Roles { get; set; }
        public ContactInformation? ContactInformation { get; set; }
    }
}
