using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreApp.Data
{
    public class CourseApply
    {   
        [Key]
        public int CourseApplyId { get; set;}

        [Required(ErrorMessage = "Lütfen bir öğrenci seçiniz")]
        public int? StudentId { get; set;}
        public Student? Student { get; set; }

        [Required(ErrorMessage = "Lütfen bir kurs seçiniz")]
        public int? CourseId { get; set;}
        public Course? Course { get; set; }

        public DateTime CourseApplyDate { get; set; }
    }
}