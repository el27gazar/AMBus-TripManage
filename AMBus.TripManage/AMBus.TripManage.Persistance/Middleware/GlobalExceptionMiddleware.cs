using AMBus.TripManage.Application.Exceptions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace AMBus.TripManage.Persistance.Middleware
{

    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(
            RequestDelegate next,
            ILogger<GlobalExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext ctx)
        {
            try
            {
                await _next(ctx);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandled: {Message}", ex.Message);
                await HandleAsync(ctx, ex);
            }
        }

        private static async Task HandleAsync(HttpContext ctx, Exception ex)
        {
            var (status, title, errors) = ex switch
            {
                NotFoundException e => (404, "غير موجود",
                    new[] { e.Message }),

                ConflictException e => (409, "تعارض في البيانات",
                    new[] { e.Message }),

                UnauthorizedException e => (401, "غير مصرح",
                    new[] { e.Message }),

                BusinessRuleException e => (422, "قاعدة عمل مخالفة",
                    new[] { e.Message }),

                ValidationException e => (400, "خطأ في البيانات",
                    e.Errors.Select(x => x.ErrorMessage).ToArray()),

                _ => (500, "خطأ داخلي في الخادم",
                    new[] { "حدث خطأ غير متوقع. حاول مرة أخرى." })
            };

            ctx.Response.ContentType = "application/json";
            ctx.Response.StatusCode = status;

            var body = JsonSerializer.Serialize(
                new { status, title, errors },
                new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });

            await ctx.Response.WriteAsync(body);
        }
    }

    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseGlobalExceptionMiddleware(
            this IApplicationBuilder app)
            => app.UseMiddleware<GlobalExceptionMiddleware>();
    }
}
