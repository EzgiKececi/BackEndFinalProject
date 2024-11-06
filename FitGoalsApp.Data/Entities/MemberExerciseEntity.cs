using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGoalsApp.Data.Entities
{
    public class MemberExerciseEntity : BaseEntity
    {
        public int MemberId { get; set; }
        public MemberEntity Member { get; set; }

        public int ExerciseId { get; set; }
        public ExerciseEntity Exercise { get; set; }

        
        
    }

    public class MemberExerciseConfiguration : BaseConfiguration<MemberExerciseEntity>
    {
        public override void Configure(EntityTypeBuilder<MemberExerciseEntity> builder)
        {
            builder.Ignore(x=>x.Id); //baseden gelen ıd yok
            builder.HasKey("MemberId", "ExerciseId"); //composite key oluşturma

            base.Configure(builder);
        }
    }
}
