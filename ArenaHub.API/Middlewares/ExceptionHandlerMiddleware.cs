namespace ArenaHub.API.Middlewares
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (UnauthorizedAccessException ex)
            {
                _logger.LogWarning(ex, ex.Message);
                httpContext.Response.StatusCode = StatusCodes.Status403Forbidden;

                await httpContext.Response.WriteAsJsonAsync(new
                {
                    ex.Message
                });
            }
            catch (InvalidOperationException ex)
            {
                _logger.LogWarning(ex, ex.Message);

                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

                await httpContext.Response.WriteAsJsonAsync(new
                {
                    error = ex.Message
                });

            }
            catch (Exception ex)
            {
                var errorId = Guid.NewGuid();
                _logger.LogError(ex, "{ErrorId} : Server Error", errorId);

                httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;

                var errorResult = new
                {
                    Id = errorId,
                    ErrorMessage = "An unexpected server error occurred."
                };

                await httpContext.Response.WriteAsJsonAsync(errorResult);
            }
        }
    }
}
