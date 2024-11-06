using FitGoalsApp.Business.Operation.Exercise;
using FitGoalsApp.Business.Operation.Exercise.Dto;
using FitGoalsApp.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FitGoalsApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExercisesController : ControllerBase
    {
        //Dependency injection uygulama
        private readonly IExerciseService _exerciseService;
        public ExercisesController(IExerciseService exerciseService)
        {
            _exerciseService = exerciseService;
        }


        [HttpPost("add-exercise")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AddExercise(AddExerciseRequest request)
        {

            if (!ModelState.IsValid)
            {
                // Hata mesajlarıyla birlikte BadRequest dönülecek
                return BadRequest(ModelState);
            }
            var addExerciseDto = new AddExerciseDto //request'ten gelen bilgiler aktarılıyor
            {
                Name = request.Name,
                DurationInMunite = request.DurationInMunite,
                Repetition = request.Repetition,
                SetCount = request.SetCount,

            };

            var result = await _exerciseService.AddExercise(addExerciseDto); //Servisteki metota aktarma

            if(result.IsSucceed) //İşlem başarılıysa result dönülecek
                return Ok(result);
            else 
                return BadRequest(result.Message); //İşlem başarısızsa mesaj ile bilgilendirme
        }
    }
}
