using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreApp.Data
{
    public class Course
    {
        // CourseId => primary key
        [Key]
        public int CourseId { get; set; }

        [Required(ErrorMessage = "Lütfen bir kurs adı belirtiniz")]
        [Display(Name = "Kurs adı")]
        public string? CourseName { get; set; }

        [Display(Name = "Ürün Görseli")]
        public string? CourseImage { get; set; }

        public ICollection<CourseApply> CourseApplies { get; set; } = new List<CourseApply>();

        [Display(Name = "Eğitmen")]
        public int EducatorId { get; set; }
        public Educator Educator { get; set; }
    }
}