using API.Models;
using System.Net;

namespace API.Middlewares
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IConfiguration _config;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger, IConfiguration config)
        {
            _next = next;
            _logger = logger;
            _config = config;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                _logger.LogError($"Error Message: {ex}\n Error Detail: {ex.InnerException?.ToString()}");
                await context.Response.WriteAsJsonAsync(new ResponseModel()
                {
                    Status = false,
                    StatusCode = context.Response.StatusCode,
                    Message = "Internal Server Error."
                });
            }
        }
    }


}