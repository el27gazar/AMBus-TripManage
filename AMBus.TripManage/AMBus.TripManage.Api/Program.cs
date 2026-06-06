using AMBus.TripManage.Api;
using AMBus.TripManage.Api.Extensions;
using AMBus.TripManage.Persistance.Middleware;
using AMBus.TripManage.Persistance.Repositories;
using Hangfire;

var builder = WebApplication.CreateBuilder(args);

var app = builder.ConfiguerService().ConfigurePipeline();
app.UseGlobalExceptionMiddleware();

app.UseHangfireDashboard();

RecurringJob.AddOrUpdate<TripCompletionJob>(
    "complete-expired-trips",
    job => job.ExecuteAsync(),
    Cron.Minutely);

await app.SeedAsync();


app.Run();