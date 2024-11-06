using FitGoalsApp.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGoalsApp.Data.Entities
{
    public class NutritionEntity : BaseEntity
    {
        public string Name { get; set; }
        public MealType MealType { get; set; }
        public int Calories { get; set; }

        // Relational properties
        public ICollection<MemberNutritionEntity> MemberNutritions { get; set; }
    }

    public class NutritionConfiguration : BaseConfiguration<NutritionEntity> 
    {
        public override void Configure(EntityTypeBuilder<NutritionEntity> builder)
        {
            base.Configure(builder);
        }
    }
}
