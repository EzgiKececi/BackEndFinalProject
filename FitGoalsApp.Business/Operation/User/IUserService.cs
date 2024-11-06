using FitGoalsApp.Business.Operation.User.Dtos;
using FitGoalsApp.Business.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGoalsApp.Business.Operation.User
{
    public interface IUserService
    {
        Task<ServiceMessage> AddUser(AddUserDto user);

        ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user);

    }
}
