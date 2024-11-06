using FitGoalsApp.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGoalsApp.Data.Entities
{
    public class ExerciseEntity : BaseEntity
    {
        public string Name { get; set; }
        public int DurationInMunite { get; set; }
        public int Repetition { get; set; }
        public int SetCount { get; set; }

        // Relational properties
        public ICollection<MemberExerciseEntity> MemberExercises { get; set; }
    }

    public class ExerciseConfiguration : BaseConfiguration<ExerciseEntity>
    {
        public override void Configure(EntityTypeBuilder<ExerciseEntity> builder)
        {

            base.Configure(builder);
        }
    }
}
