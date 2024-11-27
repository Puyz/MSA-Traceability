namespace AspNetCore.App.Middlewares
{
    public class OtherMiddleware
    {
        private readonly RequestDelegate _next;

        public OtherMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, ILogger<OtherMiddleware> logger)
        {
            var correlationId = context.Request.Headers["X-Correlation-ID"].FirstOrDefault();
            // veya
            correlationId = context.Items["CorrelationId"].ToString();

            NLog.MappedDiagnosticsContext.Set("CorrelationId", correlationId);

            logger.LogDebug("Othermiddleware Log");

            await _next(context);
        }
    }
}
