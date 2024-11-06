using FitGoalsApp.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitGoalsApp.Data.Context
{
    public class FitGoalsAppDbContext : DbContext
    {

        public FitGoalsAppDbContext(DbContextOptions<FitGoalsAppDbContext> options) : base (options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Konfigürasyonları tanımlama
            modelBuilder.ApplyConfiguration(new ExerciseConfiguration());
            modelBuilder.ApplyConfiguration(new NutritionConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new MemberConfiguration());
            modelBuilder.ApplyConfiguration(new MemberExerciseConfiguration());
            modelBuilder.ApplyConfiguration(new MemberNutritionConfiguration());

           //Seed data
            modelBuilder.Entity<SettingEntity>().HasData(
                new SettingEntity
                {
                    Id = 1,
                    MaintenenceMode=false,
                });

            base.OnModelCreating(modelBuilder);
        }

        //Entity class'larını veritabanında tabloya çevirme
        public DbSet<UserEntity> Users => Set<UserEntity>();
        public DbSet<MemberEntity> Members => Set<MemberEntity>();
        public DbSet<ExerciseEntity> Exercises => Set<ExerciseEntity>();
        public DbSet<NutritionEntity> Nutritions => Set<NutritionEntity>();
        public DbSet<MemberExerciseEntity> MemberExercises => Set<MemberExerciseEntity>();
        public DbSet<MemberNutritionEntity> MemberNutritions => Set<MemberNutritionEntity>();
        public DbSet<SettingEntity> Setting => Set<SettingEntity>();


    }
}
