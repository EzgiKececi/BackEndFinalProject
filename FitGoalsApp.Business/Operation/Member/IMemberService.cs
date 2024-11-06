
using FitGoalsApp.Business.Operation.Member.Dtos;
using FitGoalsApp.Business.Operation.UserProfile.Dtos;
using FitGoalsApp.Business.Types;
using FitGoalsApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGoalsApp.Business.Operation.Member
{
    public interface IMemberService
    {
        Task<ServiceMessage> AddMember(AddMemberDto member);
        Task<MemberDto> GetMember(int id);
        Task <MemberEntity> GetMemberByUserId(int userId);
        Task<List<MemberDto>> GetAllMembers();
        Task<ServiceMessage> AdjustWeight(int id, int newWeight);
        Task<ServiceMessage> DeleteMember(int id);
        Task<ServiceMessage> UpdateMember(UpdateMemberDto member);
    }
}
