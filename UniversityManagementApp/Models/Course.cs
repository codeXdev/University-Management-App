using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class Course
    {
        public int Id { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 5, ErrorMessage = "Code must be of 5 Chars")]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [Range(.5, 5)]
        public double Credit { get; set; }

        public string Description { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [Required]
        [Display(Name = "Semester")]
        public int SemesterId { get; set; }
        public Semester Semester { get; set; }
    }
}