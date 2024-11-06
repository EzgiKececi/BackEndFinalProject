using FitGoalsApp.Business.Operation.Nutrition.Dtos;
using FitGoalsApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGoalsApp.Business.Operation.Nutrition
{
    public interface INutritionService
    {
        Task<ServiceMessage> AddNutrition(AddNutritionDto nutrition);
    }
}
