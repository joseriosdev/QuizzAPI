namespace RESTAPI.CustomMiddlewares
{
    public class LoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggerMiddleware> _logger;

        public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext ctx)
        {
            string requestMethod = ctx.Request.Method;
            PathString requestPath = ctx.Request.Path;
            QueryString requestQueryString = ctx.Request.QueryString;

            if (requestMethod is "GET")
            {
                int responseStatusCode = ctx.Response.StatusCode;

                _logger.LogInformation($"Request: {requestMethod} {requestPath}{requestQueryString}");
                _logger.LogInformation($"Response Status Code: {responseStatusCode}");
            }
        }
    }
}
