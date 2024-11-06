using FitGoalsApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FitGoalsApp.Business.Operation.UserProfile.Dtos
{
    public class AddMemberDto
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public GoalType GoalType { get; set; }
        public int UserId { get; set; }

        public List<int> ExerciseIds { get; set; }
        public List<int> NutritionIds { get; set; }
    }
}
