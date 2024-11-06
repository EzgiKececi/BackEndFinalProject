using FitGoalsApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGoalsApp.Business.Operation.Member.Dtos
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public double Heigth { get; set; }
        public double Weight { get; set; }
        public DateTime BirthDate { get; set; }
        public GoalType GoalType { get; set; }
        public List<MemberExerciseDto> Exercises { get; set; }
        public List<MemberNutritionDto> Nutritions { get; set; }

    }
}
