using FitGoalsApp.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace FitGoalsApp.WebApi.Models
{
    public class AddExerciseRequest
    {
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public int DurationInMunite { get; set; }
        [Required]
        public int Repetition { get; set; }
        public int SetCount { get; set; }

    }
}
