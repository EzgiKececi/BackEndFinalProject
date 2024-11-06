namespace FitGoalsApp.WebApi.Middlewares
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseMaintenenceMode(this IApplicationBuilder app)
        {
            return app.UseMiddleware<MaintenenceMiddleware>();
        }

    }

    public static class LoggingMiddlewareExtensions
    {
        public static IApplicationBuilder UseLogging(this IApplicationBuilder app)
        {
            // LoggingMiddleware'i pipeline'a ekle
            return app.UseMiddleware<LoggingMiddleware>();
        }
    }
}
