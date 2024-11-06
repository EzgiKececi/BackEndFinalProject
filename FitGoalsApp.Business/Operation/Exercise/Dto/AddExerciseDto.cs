using FitGoalsApp.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGoalsApp.Business.Operation.Exercise.Dto
{
    public class AddExerciseDto
    {
        public string Name { get; set; }
        public int DurationInMunite { get; set; }
        public int Repetition { get; set; }
        public int SetCount { get; set; }
        public List<int> ExerciseIds { get; set; }
        public List<int> NutritionIds { get; set; }
    }
}
