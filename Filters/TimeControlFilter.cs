using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FitGoalsApp.WebApi.Filters
{
    public class TimeControlFilter : ActionFilterAttribute
    {
        public string StartTime { get; set; }
        public string EndTime { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var now = DateTime.Now.TimeOfDay;

            StartTime = "08:30";
            EndTime = "22:00";

            if(now >= TimeSpan.Parse(StartTime)  && now <= TimeSpan.Parse(EndTime))
            {
                base.OnActionExecuting(context);
            }
            else
            {
                context.Result = new ContentResult
                {
                    Content = "08:30 ve 22:00 arasında işlem yapabilirsiniz.",
                    StatusCode = 403,
                };
            }
         
        }
    }
}
