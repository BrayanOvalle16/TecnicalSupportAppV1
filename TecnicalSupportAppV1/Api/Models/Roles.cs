using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TecnicalSupportAppV1.Api.Models.Enums;

namespace TecnicalSupportAppV1.Api.Models
{
    public class Roles : AbstractEntityBase
    {
        public Roles(RolesEnum rol)
        {
            Rol = rol;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
        public RolesEnum Rol { get; set; }
        public List<User> Users { get; set; }
    }
}
