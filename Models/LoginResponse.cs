using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace FitGoalsApp.WebApi.Models
{
    public class LoginResponse
    {
        public string Message { get; set; }
        public string Token { get; set; }
    }
}
