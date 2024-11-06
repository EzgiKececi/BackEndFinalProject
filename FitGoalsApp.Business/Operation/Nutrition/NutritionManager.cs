using FitGoalsApp.Business.Operation.Nutrition.Dtos;
using FitGoalsApp.Business.Types;
using FitGoalsApp.Data.Entities;
using FitGoalsApp.Data.Repositories;
using FitGoalsApp.Data.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGoalsApp.Business.Operation.Nutrition
{
    public class NutritionManager : INutritionService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepository<NutritionEntity> _nutritionRepository;

        public NutritionManager(IUnitOfWork unitOfWork, IRepository<NutritionEntity> nutritionRepository)
        {
            _unitOfWork = unitOfWork;
            _nutritionRepository = nutritionRepository;
        }
        public async Task<ServiceMessage> AddNutrition(AddNutritionDto nutrition)
        {
            var hasNutrition =  _nutritionRepository.GetAll(x=>x.Name.ToLower() == nutrition.Name.ToLower()).Any();

            if (hasNutrition)
            {
                return new ServiceMessage
                {
                    IsSucceed = false,
                    Message = "Eklediğiniz öğe sistemde bulunmaktadır."
                };
                
            }

            var nutritionEntity = new NutritionEntity
            {
                Name = nutrition.Name,
                MealType = nutrition.MealType,
                Calories = nutrition.Calories,
            };

            _nutritionRepository.Add(nutritionEntity);

            try
            {
                await _unitOfWork.SaveChangesAsync();
            }
            catch (Exception)
            {

               throw new Exception("Ekleme sırasında hata oluştu.");
            }

            return new ServiceMessage
            {
                IsSucceed = true,
            };

        }
    }
}
