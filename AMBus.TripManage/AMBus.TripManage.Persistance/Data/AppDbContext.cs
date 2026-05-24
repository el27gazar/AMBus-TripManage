using AMBus.TripManage.Domain.Common;
using AMBus.TripManage.Domain.Entites;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Persistance.Data
{
    public class AppDbContext
        : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AppDbContext(
            DbContextOptions<AppDbContext> options,
            IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // ── DbSets ────────────────────────────────────────
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<Bus> Buses { get; set; }
        public DbSet<Seat> Seats { get; set; }
        public DbSet<Route> Routes { get; set; }
        public DbSet<Stop> Stops { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<BookingSeat> BookingSeats { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<ChatConversation> ChatConversations { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }

        // ══════════════════════════════════════════════════
        //  SaveChanges — Audit
        // ══════════════════════════════════════════════════

        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            ApplyAudit();
            return await base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            ApplyAudit();
            return base.SaveChanges();
        }

        private void ApplyAudit()
        {
            var currentUser =
                _httpContextAccessor.HttpContext?.User
                    .FindFirstValue(ClaimTypes.NameIdentifier)
                ?? "System";

            var now = DateTime.UtcNow;

            foreach (var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedBy = currentUser;
                        entry.Entity.CreatedDate = now;
                        entry.Entity.LastModifiedBy = currentUser;
                        entry.Entity.LastModifiedDate = now;
                        break;

                    case EntityState.Modified:
                        entry.Property(e => e.CreatedBy).IsModified = false;
                        entry.Property(e => e.CreatedDate).IsModified = false;
                        entry.Entity.LastModifiedBy = currentUser;
                        entry.Entity.LastModifiedDate = now;
                        break;
                }
            }
        }

        // ══════════════════════════════════════════════════
        //  OnModelCreating
        // ══════════════════════════════════════════════════

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

           
            builder.ApplyConfigurationsFromAssembly(
                typeof(AppDbContext).Assembly);

            

            // ── Identity tables ────────────────────────
           
            builder.Entity<User>()
                .HasMany(e => e.Claims)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);    

            builder.Entity<User>()
                .HasMany(e => e.Logins)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasMany(e => e.Tokens)
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<User>()
                .HasMany<IdentityUserRole<Guid>>()
                .WithOne()
                .HasForeignKey(e => e.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<IdentityRole<Guid>>()
                .HasMany<IdentityUserRole<Guid>>()
                .WithOne()
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<IdentityRole<Guid>>()
                .HasMany<IdentityRoleClaim<Guid>>()
                .WithOne()
                .HasForeignKey(e => e.RoleId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Driver → User ──────────────────────────
            builder.Entity<Driver>()
                .HasOne(d => d.User)
                .WithOne(u => u.Driver)
                .HasForeignKey<Driver>(d => d.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Trip → Route / Bus / Driver ────────────
            builder.Entity<Trip>()
     .HasOne(t => t.From)
     .WithMany(r => r.DepartureTrips)
     .HasForeignKey(t => t.FromId)
     .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Trip>()
    .HasOne(t => t.To)
    .WithMany(r => r.ArrivalTrips)
    .HasForeignKey(t => t.ToId)
    .OnDelete(DeleteBehavior.Restrict);

          
            // ── Booking → User / Trip ──────────────────
            builder.Entity<Booking>()
                .HasOne(b => b.User)
                .WithMany(u => u.Bookings)
                .HasForeignKey(b => b.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Booking>()
                .HasOne(b => b.Trip)
                .WithMany(t => t.Bookings)
                .HasForeignKey(b => b.TripId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Payment → Booking ─────────────────────
            builder.Entity<Payment>()
                .HasOne(p => p.Booking)
                .WithOne(b => b.Payment)
                .HasForeignKey<Payment>(p => p.BookingId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── BookingSeat → Booking / Seat ───────────
            builder.Entity<BookingSeat>()
                .HasOne(bs => bs.Booking)
                .WithMany(b => b.BookingSeats)
                .HasForeignKey(bs => bs.BookingId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<BookingSeat>()
                .HasOne(bs => bs.Seat)
                .WithMany(s => s.BookingSeats)
                .HasForeignKey(bs => bs.SeatId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Review → User / Trip ───────────────────
            builder.Entity<Review>()
                .HasOne(r => r.User)
                .WithMany(u => u.Reviews)
                .HasForeignKey(r => r.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Review>()
                .HasOne(r => r.Trip)
                .WithMany(t => t.Reviews)
                .HasForeignKey(r => r.TripId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Notification → User ────────────────────
            builder.Entity<Notification>()
                .HasOne(n => n.User)
                .WithMany(u => u.Notifications)
                .HasForeignKey(n => n.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Seat → Bus ────────────────────────────
            builder.Entity<Seat>()
                .HasOne(s => s.Bus)
                .WithMany(b => b.Seats)
                .HasForeignKey(s => s.BusId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Stop → Route ──────────────────────────
            builder.Entity<Stop>()
     .HasOne(s => s.Route)
     .WithMany(r => r.Stops)
     .HasForeignKey(s => s.RouteId)
     .OnDelete(DeleteBehavior.Restrict);

            // ── ChatConversation → User / Admin ───────────

            builder.Entity<ChatConversation>()
       .HasOne(c => c.User)
       .WithMany()
       .HasForeignKey(c => c.UserId)
       .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ChatConversation>()
                .HasOne(c => c.Admin)
                .WithMany()
                .HasForeignKey(c => c.AdminId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<ChatMessage>()
                .HasOne(m => m.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ConversationId)
                .OnDelete(DeleteBehavior.Cascade);    // لو حذفنا المحادثة الرسائل بتتحذف

            builder.Entity<ChatMessage>()
                .HasOne(m => m.Sender)
                .WithMany()
                .HasForeignKey(m => m.SenderId)
                .OnDelete(DeleteBehavior.Restrict);

            // ── Seed Roles ────────────────────────────
            builder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("11111111-1111-1111-1111-111111111111"),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("22222222-2222-2222-2222-222222222222"),
                    Name = "User",
                    NormalizedName = "USER"
                },
                new IdentityRole<Guid>
                {
                    Id = Guid.Parse("33333333-3333-3333-3333-333333333333"),
                    Name = "Driver",
                    NormalizedName = "DRIVER"
                }
            );

           
        }
    }
}
