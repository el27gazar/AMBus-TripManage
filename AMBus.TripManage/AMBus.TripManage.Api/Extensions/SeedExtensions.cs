using AMBus.TripManage.Domain.Entites;
using AMBus.TripManage.Persistance.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AMBus.TripManage.Api.Extensions
{
    public static class SeedExtensions
    {
        public static async Task SeedAsync(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var sp = scope.ServiceProvider;
            var logger = sp.GetRequiredService<ILogger<Program>>();

            try
            {
                // ── 1. Auto Migration ────────────────────
                // بيطبق الـ migrations الجديدة أوتوماتيك عند الـ startup
                var db = sp.GetRequiredService<AppDbContext>();
                await db.Database.MigrateAsync();
                logger.LogInformation("Migrations applied successfully.");

                // ── 2. Seed Roles ────────────────────────
                await SeedRolesAsync(sp, logger);

                // ── 3. Seed Admin User ───────────────────
                await SeedAdminAsync(sp, logger);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,
                    "Seeding failed: {Message}", ex.Message);
                // مش بنوقف التطبيق — بس بنعمل log
            }
        }

        // ────────────────────────────────────────────────────
        private static async Task SeedRolesAsync(
            IServiceProvider sp,
            ILogger logger)
        {
            var roleManager = sp.GetRequiredService<
                RoleManager<IdentityRole<Guid>>>();

            // FIX: Guid ثابت لكل role عشان مش يتكرر في كل startup
            var roles = new[]
            {
                (Id: "11111111-1111-1111-1111-111111111111", Name: "Admin"),
                (Id: "22222222-2222-2222-2222-222222222222", Name: "User"),
                (Id: "33333333-3333-3333-3333-333333333333", Name: "Driver"),
            };

            foreach (var (id, name) in roles)
            {
                if (await roleManager.RoleExistsAsync(name))
                    continue;

                var result = await roleManager.CreateAsync(
                    new IdentityRole<Guid>
                    {
                        Id = Guid.Parse(id),
                        Name = name,
                        NormalizedName = name.ToUpper()
                    });

                if (result.Succeeded)
                    logger.LogInformation("Role '{Name}' seeded.", name);
                else
                    logger.LogWarning(
                        "Role '{Name}' seed failed: {Errors}", name,
                        string.Join(", ", result.Errors
                            .Select(e => e.Description)));
            }
        }

        // ────────────────────────────────────────────────────
        private static async Task SeedAdminAsync(
            IServiceProvider sp,
            ILogger logger)
        {
            var userManager = sp.GetRequiredService<UserManager<User>>();
            var config = sp.GetRequiredService<IConfiguration>();

            var email = config["AdminSeed:Email"] ?? "admin@AMBus.com";
            var password = config["AdminSeed:Password"] ?? "Admin@123456";

            // FIX: FindByEmailAsync يستخدم NormalizedEmail
            // → لو المستخدم اتعمل بدون UserName صح مش هيتلاقيش
            var existing = await userManager.FindByEmailAsync(email);

            if (existing is not null)
            {
                // FIX: لو موجود بس مش عنده Role → نضيفه
                var roles = await userManager.GetRolesAsync(existing);
                if (!roles.Contains("Admin"))
                {
                    await userManager.AddToRoleAsync(existing, "Admin");
                    logger.LogInformation(
                        "Admin role assigned to existing user '{Email}'.", email);
                }
                else
                {
                    logger.LogInformation(
                        "Admin user '{Email}' already exists.", email);
                }
                return;
            }

            // FIX: UserName لازم = Email
            // FIX: EmailConfirmed = true عشان مش يحتاج email verification
            var admin = new User
            {
                Id = Guid.NewGuid(),
                FullName = "System Admin",
                Email = email,
                UserName = email,          
                EmailConfirmed = true,          
                IsActive = true,
                CreatedAt = DateTime.UtcNow
            };

            
            var createResult = await userManager.CreateAsync(admin, password);

            if (!createResult.Succeeded)
            {
                logger.LogWarning(
                    "Admin seed failed: {Errors}",
                    string.Join(", ", createResult.Errors
                        .Select(e => e.Description)));
                return;
            }

            var roleResult = await userManager.AddToRoleAsync(admin, "Admin");

            if (roleResult.Succeeded)
                logger.LogInformation(
                    "Admin user '{Email}' seeded successfully.", email);
            else
                logger.LogWarning(
                    "Admin created but role assignment failed: {Errors}",
                    string.Join(", ", roleResult.Errors
                        .Select(e => e.Description)));
        }
    }
}
