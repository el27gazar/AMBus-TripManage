using AMBus.TripManage.Api.Extensions;
using AMBus.TripManage.Application;
using AMBus.TripManage.Persistance;
using AMBus.TripManage.Persistance.Data;
using AMBus.TripManage.Persistance.Hubs;
using AMBus.TripManage.Persistance.Repositories;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AMBus.TripManage.Api
{
    public static class StartupExtension
    {
        public static WebApplication ConfiguerService(this WebApplicationBuilder builder)
        {
        
            builder.Services.AddApplicationServices();
            builder.Services.AddInfrastructureServices(builder.Configuration);
         
            builder.Services.AddAppControllers();
            builder.Services.AddAppSwagger();

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("open", policy =>
                policy.WithOrigins(builder.Configuration["FrontendUrl"] ?? "https://localhost:5005")
                .AllowAnyHeader()
                .SetIsOriginAllowed(_ => true)
                .AllowAnyMethod()
                .AllowCredentials());
            });
            builder.Services.AddSignalR();

            return builder.Build();
        
        }
        public static WebApplication ConfigurePipeline(this WebApplication app)
        {
            if (app.Environment.IsDevelopment())
                app.UseAppSwagger();
            app.UseHttpsRedirection();
            app.UseCors("open");
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHangfireDashboard("/hangfire"); 
            RecurringJob.AddOrUpdate<TripCompletionJob>(  
                "complete-expired-trips",
                job => job.ExecuteAsync(),
                Cron.Minutely);
            app.MapHub<ChatHub>("/hubs/chat");
            app.MapControllers();


            return app;
        }
        public static async Task ResetDataBase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            try
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                if(context!= null)
                {
                    context.Database.EnsureDeleted();
                    await context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred while resetting the database: {ex.Message}");
            }
        }
    }
}
