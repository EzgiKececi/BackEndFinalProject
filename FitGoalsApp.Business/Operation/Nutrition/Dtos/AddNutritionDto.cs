using FitGoalsApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGoalsApp.Business.Operation.Nutrition.Dtos
{
    public class AddNutritionDto
    {
        public string Name { get; set; }
        public MealType MealType { get; set; }
        public int Calories { get; set; }
    }
}
