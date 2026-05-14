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
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> b)
        {
            b.HasKey(x => x.Id);

            b.Property(x => x.Amount)
             .HasColumnType("decimal(10,2)")
             .IsRequired();

            b.Property(x => x.Method)
             .HasConversion<string>()
             .HasMaxLength(20);

            b.Property(x => x.Status)
             .HasConversion<string>()
             .HasMaxLength(20);

            b.Property(x => x.TransactionId)
             .HasMaxLength(100);

            b.Property(x => x.CreatedDate)
             .HasDefaultValueSql("GETUTCDATE()");
            b.Property(t => t.CreatedBy)
           .HasMaxLength(100);

            b.Property(t => t.LastModifiedBy)
             .HasMaxLength(100);
        }
    }
}
