
using FitGoalsApp.Business.Operation.Member.Dtos;
using FitGoalsApp.Business.Operation.UserProfile;
using FitGoalsApp.Business.Operation.UserProfile.Dtos;
using FitGoalsApp.Business.Types;
using FitGoalsApp.Data.Entities;
using FitGoalsApp.Data.Repositories;
using FitGoalsApp.Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGoalsApp.Business.Operation.Member
{
    
    public class MemberManager : IMemberService
    {
        //Dependency injection uygulama
        private readonly IRepository<MemberEntity> _memberRepository;
        private readonly IRepository<MemberExerciseEntity> _exerciseRepository;
        private readonly IRepository<MemberNutritionEntity> _nutritionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public MemberManager(IRepository<MemberEntity> memberRepository, IRepository<MemberExerciseEntity> exerciseRepository, IRepository<MemberNutritionEntity> nutritionRepository, IUnitOfWork unitOfWork)
        {
            _exerciseRepository = exerciseRepository;
            _memberRepository = memberRepository;
            _unitOfWork = unitOfWork;
            _nutritionRepository = nutritionRepository;
        }

      
        public async Task<ServiceMessage> AddMember(AddMemberDto member)
        {

            
            var hasMemberProfile = _memberRepository.GetAll(x=> x.UserId == member.UserId).Any(); //Profil daha önceden oluşturulmuş mu sorgusu

            if (hasMemberProfile) // True gelirse kullanıcıya bilgi verme
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Bu kullanıcı hesabının profili mevcuttur."
                };
            }

            await _unitOfWork.BeginTransaction(); //Zincirleme işlem olduğu için Transaction'ı başlatma


            var memberEntity = new MemberEntity //Tabloya entity ile bilgileri aktarma
            {
                FirstName = member.FirstName,
                LastName = member.LastName,
                BirthDate = member.BirthDate,
                Height = member.Height,
                Weight = member.Weight,
                GoalType = member.GoalType,
                UserId = member.UserId,

            };

            _memberRepository.Add(memberEntity); //Veritabanına ekleme
            
            try
            {
                await _unitOfWork.SaveChangesAsync(); //İşlem başarlıysa kaydetme
            }
            catch (Exception)
            {

                throw new Exception("Profil oluşturma sırasında bir sorunla karşılaşıldı." ); //Hata durumunda exception fırlatma
            }

            foreach (var exerciseId in member.ExerciseIds) //Birden fazla egzersiz bilgisi eklemek için foreach döngüsü kullanılacak
            {
                var memberExercise = new MemberExerciseEntity
                {
                    MemberId = memberEntity.Id,
                    ExerciseId = exerciseId,
                };

                _exerciseRepository.Add(memberExercise); 
            }

            try
            {
                await _unitOfWork.SaveChangesAsync(); //Hata yoksa kaydetme işlemi
                
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction(); //Hata varsa en başa dönecek

                throw new Exception("Egzersiz özellikleri eklerken bir hatayla karışılaşıldı.");
            }

            foreach (var nutritionId in member.NutritionIds) //Birden fazla beslenme bilgisi eklemek için foreach döngüsü kullanılacak
            {
                var memberNutrition = new MemberNutritionEntity
                {
                    MemberId = memberEntity.Id,
                    NutritionId = nutritionId,
                };

                _nutritionRepository.Add(memberNutrition);
            }

            try
            {
                await _unitOfWork.SaveChangesAsync(); //Kaydetme işlemi
                await _unitOfWork.CommitTransaction(); //Zincirleme işlemi bittiği için çalıştırılacak
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction(); //Hata varsa en başa dönecek

                throw new Exception("Beslenme özellikleri eklerken bir hatayla karışılaşıldı.");
            }



            return new ServiceMessage
            {
                IsSucceed = true,

            };


        }

      

        public async Task<List<MemberDto>> GetAllMembers()
        {
            var members = await _memberRepository.GetAll() //Tüm verileri alma
               .Select(x => new MemberDto 
               {
                   Id = x.Id,
                   FirstName = x.FirstName,
                   LastName = x.LastName,
                   BirthDate = x.BirthDate,
                   Heigth = x.Height,
                   Weight = x.Weight,
                   GoalType = x.GoalType,
                   Exercises = x.MemberExercises.Select(e => new MemberExerciseDto
                   {
                       Id = e.Id,
                       Name = e.Exercise.Name 
                   }).ToList(),

                   Nutritions = x.MemberNutritions.Select(n => new MemberNutritionDto
                   {
                       Id = n.Id,
                       MealType = n.Nutrition.MealType,
                       Calories = n.Nutrition.Calories,
                       Name = n.Nutrition.Name

                   }).ToList()
               }).ToListAsync();

            return members;
        }

        public async Task<MemberDto> GetMember(int id)
        {
            var member = await _memberRepository.GetAll(x => x.Id == id)
                .Select(x => new MemberDto
                {
                    Id = x.Id,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    BirthDate = x.BirthDate,
                    Heigth = x.Height,
                    Weight = x.Weight,
                    GoalType = x.GoalType,
                    Exercises = x.MemberExercises.Select(e => new MemberExerciseDto
                    {
                        Id = e.Id,
                        Name = e.Exercise.Name
                    }).ToList(),
                   
                    Nutritions = x.MemberNutritions.Select(n => new MemberNutritionDto
                    {
                        Id = n.Id,
                        MealType = n.Nutrition.MealType,
                        Calories = n.Nutrition.Calories,
                        Name=n.Nutrition.Name

                        
                    }).ToList()
                }).FirstOrDefaultAsync();

            return member;
        }

        public async Task<ServiceMessage> AdjustWeight(int id, int newWeight)
        {
            var updatedMember = _memberRepository.GetById(id);

            if (updatedMember is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "İlgili üye kaydı bulunamadı."
                };
            }

            updatedMember.Weight = newWeight;

            _memberRepository.Update(updatedMember);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Güncelleme sırasında bir hata oluştu.");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Güncelleme işlemi başarılı."
            };

        }

        public async Task<ServiceMessage> DeleteMember(int id)
        {

            var deletedMember = _memberRepository.GetById(id);
            if (deletedMember is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Kayıtlı üye bulunamadı."
                };
            }

            _memberRepository.Delete(id);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

                throw new Exception("Üye silme işlemi sırasında bir hata oluştu.");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
                Message = "Silme işlemi tamamlandı."
            };
        }

        public async Task<ServiceMessage> UpdateMember(UpdateMemberDto member)
        {
            var memberEntity = _memberRepository.GetById(member.Id);
            if (memberEntity is null)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Üye bulunamadı."
                };
            }

            await _unitOfWork.BeginTransaction();
            
            memberEntity.FirstName = member.FirstName;
            memberEntity.LastName = member.LastName;
            memberEntity.BirthDate = member.BirthDate;
            memberEntity.Weight = member.Weight;
            memberEntity.Height = member.Height;
            memberEntity.GoalType = member.GoalType;
            memberEntity.UserId = member.UserId;
            
            _memberRepository.Update(memberEntity);


            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Üye güncellemesi sırasında bir hata oluştu.");
            }

            var memberExercises = _exerciseRepository.GetAll(x=> x.ExerciseId == x.ExerciseId).ToList();
            foreach (var memberExercise in memberExercises)
            { 
                _exerciseRepository.Delete(memberExercise, false); //Hard Delete ile silme 
            }

            foreach (var exerciseId in member.ExerciseIds)
            {
                var memberExercise = new MemberExerciseEntity
                {
                    MemberId = memberEntity.Id,
                    ExerciseId = exerciseId
                };

                _exerciseRepository.Add(memberExercise);
            }
            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();
                throw new Exception("Egzersiz bilgileri güncellenirlen bir hata ile karşılaşıldı.");
            }

            var memberNutritions = _nutritionRepository.GetAll(x => x.NutritionId == x.NutritionId).ToList();
            foreach (var memberNutrition in memberNutritions)
            {
                _nutritionRepository.Delete(memberNutrition, false); //Hard Delete ile silme 
            }

            foreach (var nutritionId in member.NutritionIds)
            {
                var memberNutrition = new MemberNutritionEntity
                {
                    MemberId = memberEntity.Id,
                    NutritionId = nutritionId
                    
                };

                _nutritionRepository.Add(memberNutrition);
            }


            try
            {
                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitTransaction();
            }
            catch (Exception)
            {
                await _unitOfWork.RollBackTransaction();

                throw new Exception("Beslenme bilgilerli güncellenirken bir hata ile karşılaşıldı.");
            }


            return new ServiceMessage
            {
                IsSucceed = true,
            };

        }

        public async Task<MemberEntity> GetMemberByUserId(int userId)
        {
            return await _memberRepository.GetAll(x => x.UserId == userId).FirstOrDefaultAsync();
        }
    }
    }


