using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AMBus.TripManage.Application.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse>
         : IPipelineBehavior<TRequest, TResponse>
         where TRequest : IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(
            ILogger<LoggingBehavior<TRequest, TResponse>> logger)
            => _logger = logger;

        public async Task<TResponse> Handle(
            TRequest request,
            RequestHandlerDelegate<TResponse> next,
            CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var sw = Stopwatch.StartNew();

            _logger.LogInformation(
                "[START] {Request} — {Time}",
                requestName,
                DateTime.UtcNow.ToString("HH:mm:ss"));

            try
            {
                var response = await next();
                sw.Stop();

                _logger.LogInformation(
                    "[END] {Request} — {Elapsed}ms",
                    requestName,
                    sw.ElapsedMilliseconds);

                return response;
            }
            catch (Exception ex)
            {
                sw.Stop();
                _logger.LogError(ex,
                    "[ERROR] {Request} — {Elapsed}ms — {Message}",
                    requestName,
                    sw.ElapsedMilliseconds,
                    ex.Message);
                throw;
            }
        }
    }
}
