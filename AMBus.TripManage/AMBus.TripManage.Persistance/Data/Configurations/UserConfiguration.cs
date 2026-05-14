using AMBus.TripManage.Domain.Entites;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Persistance.Data.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> b)
        {
            b.Property(u => u.FullName)
             .HasMaxLength(100)
             .IsRequired();

            b.Property(u => u.IsActive)
             .HasDefaultValue(true);

            b.Property(u => u.CreatedAt)
             .HasDefaultValueSql("GETUTCDATE()");
        }
    }
}
