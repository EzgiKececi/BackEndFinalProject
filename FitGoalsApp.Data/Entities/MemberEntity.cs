using FitGoalsApp.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FitGoalsApp.Data.Entities
{
    public class MemberEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public GoalType GoalType { get; set; }

        [JsonIgnore]
        public int UserId { get; set; }

        // Relational properties
        public UserEntity User { get; set; }
        public ICollection<MemberExerciseEntity> MemberExercises { get; set; } 
        public ICollection<MemberNutritionEntity> MemberNutritions { get; set; }

    }

    public class MemberConfiguration : BaseConfiguration<MemberEntity>
    {
        public override void Configure(EntityTypeBuilder<MemberEntity> builder)
        {
             
            
            builder.Property(x => x.FirstName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.LastName)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(x => x.Weight)
                .IsRequired();

            builder.Property(x => x.Height)
                .IsRequired();

            base.Configure(builder);
        }
    }
}
