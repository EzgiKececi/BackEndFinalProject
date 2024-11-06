using FitGoalsApp.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace FitGoalsApp.WebApi.Models
{
    public class AddNutritionRequest
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public MealType MealType { get; set; }
        [Required]
        public int Calories { get; set; }
    }
}
