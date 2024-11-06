using FitGoalsApp.Business.Operation.Exercise.Dto;
using FitGoalsApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGoalsApp.Business.Operation.Exercise
{
    public interface IExerciseService
    {
        Task<ServiceMessage> AddExercise(AddExerciseDto exercise);
    }
}
