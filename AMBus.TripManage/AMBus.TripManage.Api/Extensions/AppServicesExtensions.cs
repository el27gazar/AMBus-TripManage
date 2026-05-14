using System.Text.Json.Serialization;

namespace AMBus.TripManage.Api.Extensions
{
    public static class AppServicesExtensions
    {
        public static IServiceCollection AddAppControllers(
            this IServiceCollection services)
        {
            services
                .AddControllers()
                .AddJsonOptions(opt =>
                {
                    opt.JsonSerializerOptions.Converters
                        .Add(new JsonStringEnumConverter());

                    opt.JsonSerializerOptions.DefaultIgnoreCondition =
                        JsonIgnoreCondition.WhenWritingNull;

                    opt.JsonSerializerOptions.PropertyNamingPolicy =
                        System.Text.Json.JsonNamingPolicy.CamelCase;
                });

            services.AddEndpointsApiExplorer();
            return services;
        }
    }
}
