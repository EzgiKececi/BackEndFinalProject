namespace FitGoalsApp.WebApi.Middlewares
{
    public class LoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<LoggingMiddleware> _logger;

        public LoggingMiddleware(RequestDelegate next, ILogger<LoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // İstek URL'si ve zaman bilgisini al
            var requestPath = context.Request.Path;
            var requestTime = DateTime.UtcNow;

            // Kimlik doğrulama bilgisini al (Kullanıcı adı veya IP adresi)
            var user = context.User.Identity.IsAuthenticated ? context.User.Identity.Name : context.Connection.RemoteIpAddress.ToString();

            // Loglama yap
            _logger.LogInformation($"İstek yapan kullanıcı: {user}, İstek zamanı: {requestTime}, İstek yapılan URL: {requestPath}");

            // İstek işlenmeye devam eder
            await _next(context);
        }
    }
}
