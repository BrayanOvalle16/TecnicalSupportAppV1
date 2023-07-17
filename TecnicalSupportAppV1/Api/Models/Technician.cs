using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecnicalSupportAppV1.Api.Models
{
    [Index(nameof(UserId), IsUnique = true)]
    public class Technician : AbstractEntityBase
    {
        public long Id { get; set; }
        public User User { get; set; }
        public Office Office { get; set; }
        [ForeignKey(nameof(User))]
        public long UserId { get; set; }
        [ForeignKey(nameof(Office))]
        public long OfficeId { get; set; }
        public ICollection<Stock> Stocks { get; set; }
    }
}
