using FitGoalsApp.Business.Operation.Exercise.Dto;
using FitGoalsApp.Business.Types;
using FitGoalsApp.Data.Entities;
using FitGoalsApp.Data.Repositories;
using FitGoalsApp.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGoalsApp.Business.Operation.Exercise
{
    public class ExerciseManager : IExerciseService
    {
        //Dependency injection uygulamam
        private readonly IRepository<ExerciseEntity> _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ExerciseManager(IRepository<ExerciseEntity> repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }
        public async Task<ServiceMessage> AddExercise(AddExerciseDto exercise)
        {
            var hasExercise = _repository.GetAll(x=>x.Name.ToLower() == exercise.Name.ToLower()).Any(); //Tabloda eşleşme var mı sorgusu

            if(hasExercise) //True değer aldığında mesaj ile bilgi verme
            {
                return new ServiceMessage
                {
                    Message = "Eklemek istediğiniz egzersiz, sistemde kayıtlıdır.",
                    IsSucceed = false

                };
            }

            var exerciseEntity = new ExerciseEntity //Tabloya verileri entity ile aktarma
            {
                Name = exercise.Name,
                DurationInMunite = exercise.DurationInMunite,
                Repetition=exercise.Repetition,
                SetCount=exercise.SetCount,
            };

            _repository.Add(exerciseEntity); //Veritabanına ekleme

            try
            {
                await _unitOfWork.SaveChangesAsync(); //Hata yoksa kaydetme işlemi
            }
            catch (Exception)
            {

                throw new Exception("Ekleme sırasında bir hata oluştu."); //Hata durumunda exception fırlatma
            }

            //Ekleme başarılıysa
            return new ServiceMessage 
            {
                IsSucceed = true,
            };


        }
    }
}
