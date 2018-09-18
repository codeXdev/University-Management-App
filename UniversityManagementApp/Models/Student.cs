using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class Student
    {
        public int Id { get; set; }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }

        [EmailAddress]
        [Required]
        [StringLength(255)]
        public string Email { get; set; }

        [Phone]
        [Required]
        [StringLength(11)]
        [Display(Name = "Contact No")]
        public string ContactNo { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [StringLength(500)]
        public string Address { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [StringLength(12, MinimumLength = 12, ErrorMessage = "Must be of 12 chars")]
        public string RegistrationNo { get; set; }
    }
}