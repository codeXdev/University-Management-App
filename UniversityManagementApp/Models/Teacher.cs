using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class Teacher
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Contact No")]
        public string ContactNo { get; set; }

        [Required]
        [Display(Name = "Designation")]
        public int DesignationId { get; set; }
        public Designation Designation { get; set; }

        [Required]
        [Display(Name = "Department")]
        public int DepartmentId { get; set; }
        public Department Department { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        [Display(Name = "Credit to be Taken")]
        public double CreditToTake { get; set; }
    }
}