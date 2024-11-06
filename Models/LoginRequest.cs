using System.ComponentModel.DataAnnotations;

namespace FitGoalsApp.WebApi.Models
{
    public class LoginRequest
    {
        [Required]
        [EmailAddress]
        [StringLength(50)]
        public string Email { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 6)]
        public string Password { get; set; }
    }
}
