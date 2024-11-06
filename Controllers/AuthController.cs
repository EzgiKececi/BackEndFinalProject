using FitGoalsApp.Business.Operation.User;
using FitGoalsApp.Business.Operation.User.Dtos;
using FitGoalsApp.WebApi.Filters;
using FitGoalsApp.WebApi.Jwt;
using FitGoalsApp.WebApi.Models;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;

namespace FitGoalsApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    
    public class AuthController : ControllerBase
    {
        // Dependency injection uygulama
        private readonly IUserService _userService; 

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

       
        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequest request)
        {
            if (!ModelState.IsValid)
            {
                // Hata mesajlarıyla birlikte BadRequest dönülecek
                return BadRequest(ModelState);
            }
            var adduserDto = new AddUserDto // request'ten gelen bilgilerin aktarılması
            {
                Email = request.Email,
                Password = request.Password,
            };

            var result = await _userService.AddUser(adduserDto);

            if(result.IsSucceed) // İşlem başarılıysa result dönülecek
                return Ok(result);
            else
                return BadRequest(result.Message); // İşlemin başarısız olduğu mesajı verilecek
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            if (!ModelState.IsValid)
            {
                // Hata mesajlarıyla birlikte BadRequest dönülecek
                return BadRequest(ModelState);
            }

            var loginUser = new LoginUserDto // request'ten gelen bilgilerin aktarılması
            {
                Email = request.Email,
                Password = request.Password,
            };

            var result = _userService.LoginUser(loginUser);

            if (!result.IsSucceed)
                return BadRequest(result.Message); // İşlemin başarısız olduğu mesajı verilecek

            var user = result.Data; // Generic olan Data bilgisi çekiliyor

            var configuration = HttpContext.RequestServices.GetRequiredService<IConfiguration>(); //appsettings'teki bilgileri alabilmek için

            var token = JwtHelper.GenerateJwtToken(new JwtDto //token'a user'daki bilgiler aktarılıyor
            {
                Id = user.Id,
                Email = user.Email,
                UserType = user.UserType,
                SecretKey = configuration["Jwt:SecretKey"],
                Issuer = configuration["Jwt:Issuer"],
                Audience = configuration["Jwt:Audience"],
                ExpireMinutes = int.Parse(configuration["Jwt:ExpireMinutes"])

            });


            return Ok(new LoginResponse // Kullanıcıya token ile response dönülüyor
            {
                Message = "Giriş başarıyla tamamlandı.",
                Token = token
            });

        }
    }
}
