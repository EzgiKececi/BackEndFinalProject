using FitGoalsApp.Business.Operation.UserProfile.Dtos;
using FitGoalsApp.WebApi.Jwt;
using FitGoalsApp.WebApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FitGoalsApp.Business.Operation.Member;
using FitGoalsApp.Business.Operation.Member.Dtos;
using System.Runtime.CompilerServices;
using FitGoalsApp.WebApi.Filters;
using FitGoalsApp.Data.Entities;

namespace FitGoalsApp.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        //Dependency injection uygulama
        private readonly IMemberService _memberService;

        public MembersController(IMemberService memberService)
        {
            _memberService = memberService;

        }

        [HttpPost("add-member")]
        
        public async Task<IActionResult> AddMember(AddMemberRequest request)
        {
            var userIdClaim = User.FindFirst(JwtClaimNames.Id); //JWT'den Id'yi çekme

            // Claim değerini almak ve uygun türde dönüştürmek
            var userId = int.Parse(userIdClaim.Value);


            var addMemberDto = new AddMemberDto //request'ten gelen bilgileri aktarma
            {
                
                FirstName = request.FirstName,
                LastName = request.LastName,
                Height = request.Height,
                Weight = request.Weight,
                GoalType = request.GoalType,
                ExerciseIds = request.ExerciseIds,
                NutritionIds = request.NutritionIds,
                UserId = userId, //JWT'den çekilen Id'yi atama
            };

            var result = await _memberService.AddMember(addMemberDto); //Servisteki metodu çağırma

            if (!result.IsSucceed)
            {
                return BadRequest(result.Message);
            }
            else
                return Ok(result);
        }

        

        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetMember(int id)
        {
            var userIdClaim = User.FindFirst(JwtClaimNames.Id); //JWT'den Id'yi çekme

            //Claim kontrolü
            if (userIdClaim == null)
            {
                return Unauthorized("Kullanıcı kimliği bulunamadı.");
            }

            var userId = int.Parse(userIdClaim.Value);

            //Kullanıcının üye bilgilerini al
            var member = await _memberService.GetMemberByUserId(userId);

            // Rol kontrolü: Eğer kullanıcı admin ise ya da kendi bilgilerini sorguluyorsa erişim izni ver
            if (!User.IsInRole("Admin") && member.Id != id)
            {
                return Forbid("Bu işlemi gerçekleştirmek için yeterli izniniz yok.");
            }

            // Belirtilen id'ye göre member bilgilerini al
            var requestedMember = await _memberService.GetMember(id);

            if (requestedMember is null)
                return NotFound();

            return Ok(requestedMember);
        }

        

        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAllMembers()
        {
            var members = await _memberService.GetAllMembers();

            return Ok(members);

        }

        [HttpPatch("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AdjustWeight(int id, int newWeight)
        {
            var result = await _memberService.AdjustWeight(id, newWeight); //Servisteki metodu çağırma

            if (!result.IsSucceed)
                return NotFound(result.Message);
            else
                return Ok(result);

        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _memberService.DeleteMember(id); //Servisteki metodu çağırma 

            if (!result.IsSucceed)
                return NotFound(result.Message);
            else
                return Ok();

        }

        [HttpPut("{id}")]
        [Authorize(Roles ="Admin")]
        [TimeControlFilter]
     
        public async Task<IActionResult> UpdateMember(int id, UpdateMemberRequest request )
        {
           

            var updatedMemberDto = new UpdateMemberDto // request'ten gelen bilgileri aktarma
            {
                Id = id,
                FirstName = request.FirstName,
                LastName = request.LastName,
                BirthDate = request.BirthDate,
                Height = request.Height,
                Weight = request.Weight,
                GoalType = request.GoalType,
                ExerciseIds = request.ExerciseIds,
                NutritionIds = request.NutritionIds,
                UserId = id          
            };

            var result = await _memberService.UpdateMember(updatedMemberDto);

            if (!result.IsSucceed)
            {
                return NotFound(result.Message);
            }
            else
            {
                return await GetMember(id);
            }

        }
    }
}

