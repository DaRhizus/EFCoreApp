using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using EFCoreApp.Data;

namespace EFCoreApp.Models
{
    public class CourseViewModel
    {
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Lütfen bir kurs adı belirtiniz")]
        [Display(Name = "Kurs adı")]
        public string? CourseName { get; set; }

        [Display(Name = "Ürün Görseli")]
        public string? CourseImage { get; set; }

        [Display(Name = "Eğitmen Adı")]
        public int EducatorId { get; set; }

        public ICollection<CourseApply> CourseApplies { get; set; } = new List<CourseApply>();
    }
}