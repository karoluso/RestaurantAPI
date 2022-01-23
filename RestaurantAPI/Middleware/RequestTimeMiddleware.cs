using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Threading.Tasks;

namespace RestaurantAPI.Middleware
{
    public class RequestTimeMiddleware : IMiddleware
    {
        private readonly ILogger<RequestTimeMiddleware> _logger;
        private Stopwatch _stopwatch;
        public RequestTimeMiddleware(ILogger<RequestTimeMiddleware> logger)
        {
            _logger = logger;
            _stopwatch= new Stopwatch();
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _stopwatch.Start();
            await next.Invoke(context);
            _stopwatch.Stop();
            var elapsedTime = _stopwatch.Elapsed.TotalMilliseconds;

            if (elapsedTime > 4000)
            {
                var message = $"" +
                    $"Request [{context.Request.Method}] at  [{context.Request.Path}] - time elapsed : {elapsedTime} ms";
               
                _logger.LogInformation(message);
            }
        }
    }
}
