using FitGoalsApp.Business.Operation.Nutrition;
using FitGoalsApp.Business.Operation.Nutrition.Dtos;
using FitGoalsApp.Business.Types;
using FitGoalsApp.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitGoalsApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionsController : ControllerBase
    {
        //Dependency injection uygulama
        private readonly INutritionService _nutritionService;
        public NutritionsController(INutritionService nutritionService)
        {
            _nutritionService = nutritionService;
        }

        [HttpPost("add-nutrition")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> AddNutrition(AddNutritionRequest request)
        {

            if (!ModelState.IsValid)
            {
                // Hata mesajlarıyla birlikte BadRequest dönüyoruz.
                return BadRequest(ModelState);
            }
            var nutritionDto = new AddNutritionDto // request'ten gelen bilgileri aktarma
            {
                Name = request.Name,
                MealType = request.MealType,
                Calories = request.Calories,
            };

            var result = await _nutritionService.AddNutrition(nutritionDto); //Servisteki metodu çağırma

            if (result.IsSucceed)
                return Ok(result);

            else
                return BadRequest(result);

        }
    }
}
