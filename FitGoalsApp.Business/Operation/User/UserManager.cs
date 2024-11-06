using FitGoalsApp.Business.DataProtection;
using FitGoalsApp.Business.Operation.User.Dtos;
using FitGoalsApp.Business.Types;
using FitGoalsApp.Data.Entities;
using FitGoalsApp.Data.Enums;
using FitGoalsApp.Data.Repositories;
using FitGoalsApp.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGoalsApp.Business.Operation.User
{
    public class UserManager : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<UserEntity> _userRepository;
        private readonly IDataProtection _dataProtector;

        public UserManager(IUnitOfWork unitOfWork, IRepository<UserEntity> userRepository, IDataProtection dataProtector)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _dataProtector = dataProtector;
            
        }
        public async Task<ServiceMessage> AddUser(AddUserDto user)
        {
            var hasMail = _userRepository.GetAll(x=>x.Email.ToLower()==user.Email.ToLower()).Any();

            if (hasMail)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu email adresi ile bir kullanıcı bulunmaktadır."
                };
            }

            var userEntity = new UserEntity
            {
                Email = user.Email,
                Password = _dataProtector.Protect(user.Password),
                UserType = UserType.User,
                
            };

            _userRepository.Add(userEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Kullanıcı kaydı sırasında bir hata oluştu.");
            }

            return new ServiceMessage
            {
                IsSucceed = true
            };
        }

        public ServiceMessage<UserInfoDto> LoginUser(LoginUserDto user)
        {
            var userEntity = _userRepository.Get(x=>x.Email.ToLower() == user.Email.ToLower());

            if (userEntity == null)
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Kullanıcı adı ya da parola hatalı."
                };
            }

            if (user.Password == _dataProtector.UnProtect(userEntity.Password)) 
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = true,
                    Data = new UserInfoDto
                    {
                        Id = userEntity.Id,
                        Email = userEntity.Email,
                        Password = userEntity.Password,
                        UserType = userEntity.UserType,
                    }
                };

            }
            else
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Kullanıcı adı ya da parola hatalı."
                };

            }
                    
        }
    }
}
