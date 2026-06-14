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
            b.Property(x => x.Amount).HasColumnType("decimal(10,2)").IsRequired();
            b.Property(x => x.Currency).HasMaxLength(10).HasDefaultValue("EGP");
            b.Property(x => x.Method).HasConversion<string>().HasMaxLength(30);
            b.Property(x => x.Status).HasConversion<string>().HasMaxLength(30);
            b.Property(x => x.Provider).HasConversion<string>().HasMaxLength(20);
            b.Property(x => x.StripeClientSecret).HasMaxLength(2000);
            b.Property(x => x.ReferenceNumber).HasMaxLength(100);
            b.Property(x => x.StripeClientSecret).HasMaxLength(2000);
            b.Property(x => x.WalletMsisdn).HasMaxLength(20);
            b.Property(x => x.WalletRedirectUrl).HasMaxLength(500);
            b.Property(x => x.FawryReferenceNumber).HasMaxLength(100);
            b.Property(x => x.OtcReferenceNumber).HasMaxLength(100);
            b.Property(x => x.ExternalTransactionId).HasMaxLength(100);
            b.Property(x => x.CreatedBy).HasMaxLength(100);
            b.Property(x => x.LastModifiedBy).HasMaxLength(100);

            b.HasIndex(x => x.StripeClientSecret);
            b.HasIndex(x => x.ReferenceNumber);
            b.HasIndex(x => x.FawryReferenceNumber);

            b.HasOne(x => x.Booking)
             .WithOne(bk => bk.Payment)
             .HasForeignKey<Payment>(x => x.BookingId)
             .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
