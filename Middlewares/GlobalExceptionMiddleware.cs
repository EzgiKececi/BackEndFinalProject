using System.Net;
using System.Text.Json;

namespace FitGoalsApp.WebApi.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        public class ExceptionHandlingMiddleware
        {
            private readonly RequestDelegate _next;
            public ExceptionHandlingMiddleware(RequestDelegate next)
            {
                _next = next;
            }
            public async Task InvokeAsync(HttpContext context)
            {
                try
                {
                    await _next(context);
                }
                catch (Exception ex)
                {
                    await HandleExceptionAsync(context, ex);
                }
            }
            private static Task HandleExceptionAsync(HttpContext context, Exception exception)
            {
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                var response = new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = "Sunucuda bir hata oluştu. Lütfen daha sonra tekrar deneyiniz.",
                    Detailed = exception.Message // Detaylı hata mesajını loglarda görmek için
                };
                var jsonResponse = JsonSerializer.Serialize(response);
                return context.Response.WriteAsync(jsonResponse);
            }
        }
    }
}
