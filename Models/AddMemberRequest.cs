using FitGoalsApp.Data.Entities;
using FitGoalsApp.Data.Enums;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FitGoalsApp.WebApi.Models
{
    public class AddMemberRequest
    {
        [Required]
        [StringLength(20)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(20)]
        public string LastName { get; set; }
        [Required]
        public DateTime BirthDate { get; set; }
        [Required]
        public double Height { get; set; }
        [Required]
        public double Weight { get; set; }
        [Required]
        public GoalType GoalType { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }

        public List<int> ExerciseIds { get; set; }
        public List<int> NutritionIds { get; set; }
   
    }
}
