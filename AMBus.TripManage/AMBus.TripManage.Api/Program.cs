using AMBus.TripManage.Api;
using AMBus.TripManage.Api.Extensions;
using AMBus.TripManage.Persistance.Middleware;

var builder = WebApplication.CreateBuilder(args);

var app = builder.ConfiguerService().ConfigurePipeline();
app.UseGlobalExceptionMiddleware();


  await app.SeedAsync();


app.Run();