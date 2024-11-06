using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGoalsApp.Data.Entities
{
    public class MemberNutritionEntity : BaseEntity
    {
        public int MemberId { get; set; }
        public MemberEntity Member { get; set; }

        public int NutritionId { get; set; }
        public NutritionEntity Nutrition { get; set; }

       
    }
    public class MemberNutritionConfiguration : BaseConfiguration<MemberNutritionEntity>
    {
        public override void Configure(EntityTypeBuilder<MemberNutritionEntity> builder)
        {
            builder.Ignore(x => x.Id); //baseden gelen ıd yok
            builder.HasKey("MemberId", "NutritionId"); //composite key oluşturma

            base.Configure(builder);
        }
    }
}
