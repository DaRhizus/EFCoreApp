using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EFCoreApp.Data
{
    public class Educator
    {
        [Key]
        [Display(Name = "Eğitmen ID")]
        public int EducatorId { get; set; }

        [Required(ErrorMessage = "Lütfen bir eğitmen adı giriniz")]
        [Display(Name = "Eğitmen Adı")]
        public string? EducatorFirstName { get; set; }

        [Required(ErrorMessage = "Lütfen bir eğitmen soyadı giriniz")]
        [Display(Name = "Eğitmen soyadı")]
        public string? EducatorLastName { get; set; }

        [Display(Name = "Eğitmen Ad-Soyad")]
        public string? EducatorFullName { 
            get{
                return this.EducatorFirstName + " " + this.EducatorLastName;
            } 
        }

        [Required(ErrorMessage = "Lütfen bir eğitmen e-postası giriniz")]
        [Display(Name = "Eğitmen E-postası")]
        public string? EducatorEmail { get; set; }

        public ICollection<Course> Courses { get; set; } = new List<Course>();
        
    }
}