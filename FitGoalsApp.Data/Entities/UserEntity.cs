using FitGoalsApp.Data.Enums;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGoalsApp.Data.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public UserType UserType { get; set; }

        //Relational property
        public MemberEntity Member { get; set; }

    }

    public class UserConfiguration : BaseConfiguration<UserEntity>
    {
        public override void Configure(EntityTypeBuilder<UserEntity> builder)
        {
          
            base.Configure(builder);
        }
    }
}
