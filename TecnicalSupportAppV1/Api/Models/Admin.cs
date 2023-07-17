﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace TecnicalSupportAppV1.Api.Models
{
    [Index(nameof(UserId), IsUnique = true)]
    public class Admin : AbstractEntityBase
    {
        public long Id { get; set; }

        public User User { get; set; }
        [ForeignKey(nameof(User))]
        public long UserId { get; set; }
    }
}
