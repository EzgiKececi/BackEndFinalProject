using FitGoalsApp.Business.Operation.Setting;

namespace FitGoalsApp.WebApi.Middlewares
{
    public class MaintenenceMiddleware
    {
        private readonly RequestDelegate _next;
       
        public MaintenenceMiddleware(RequestDelegate next)
        {
            _next = next;

        }

        public async Task Invoke(HttpContext context)
        {
            var settingService = context.RequestServices.GetRequiredService<ISettingService>();
            bool maintanenceMode = settingService.GetMaintenenceState();

            if (context.Request.Path.StartsWithSegments("/api/Auth/login") || (context.Request.Path.StartsWithSegments("/api/Settings")))
            {
                await _next(context);
                return;
            }

            if (maintanenceMode)
            {
                await context.Response.WriteAsync("Şu anda hizmet verilmemektedir.");
            }
            else
            {
                await _next(context);
            }
        }
    }
}
