using AMBus.TripManage.Domain.Common;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Domain.Entites
{
    public class Role : IdentityRole<Guid>
    {
        public Guid Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = string.Empty;  // "Admin" | "User" | "Driver"

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
