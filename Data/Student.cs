using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreApp.Data
{
    public class Student
    {
        // StudentId => primary key
        [Key]
        [Display(Name = "Öğrenci ID")]
        public int StudentId { get; set; }


        [Required(ErrorMessage = "Lütfen bir öğrenci adı giriniz")]
        [Display(Name = "Öğrenci Adı")]
        public string? StudentFirstName { get; set; }


        [Required(ErrorMessage = "Lütfen bir öğrenci soyadı giriniz")]
        [Display(Name = "Öğrenci Soyadı")]
        public string? StudentLastName { get; set; }


        [Required(ErrorMessage = "Lütfen bir öğrenci e-postası giriniz")]
        [Display(Name = "Öğrenci E-posta")]
        public string? StudentEmail { get; set; }


        [Required(ErrorMessage = "Lütfen bir öğrenci telefonu giriniz")]
        [Display(Name = "Öğrenci Telefon")]
        public string? StudentPhone { get; set; }

        [Display(Name = "Öğrenci Ad-Soyad")]
        public string StudentFullName { 
            get{
                return this.StudentFirstName + " " + this.StudentLastName;
            }
        
        }

        public ICollection<CourseApply> CourseApplies { get; set; } = new List<CourseApply>();

    }
}