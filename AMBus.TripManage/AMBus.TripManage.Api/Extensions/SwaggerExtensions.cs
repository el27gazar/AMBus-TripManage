using Microsoft.OpenApi.Models;

namespace AMBus.TripManage.Api.Extensions
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddAppSwagger(
            this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "AMBus TripManage API",
                    Version = "v1",
                    Description = "AMBus TripManage API Documentation"
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Bearer {token}"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {{
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id   = "Bearer"
                        }
                    },
                    Array.Empty<string>()
                }});
            });

            return services;
        }

        public static IApplicationBuilder UseAppSwagger(
            this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "AMBus API v1");
                c.RoutePrefix = string.Empty;
                c.DisplayRequestDuration();
            });

            return app;
        }
    }
}
                                                       