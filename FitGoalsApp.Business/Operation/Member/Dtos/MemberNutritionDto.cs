using FitGoalsApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGoalsApp.Business.Operation.Member.Dtos
{
    public class MemberNutritionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public MealType MealType { get; set; }
        public int Calories { get; set; }
    }
}
