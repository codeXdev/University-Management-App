using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class Department
    {
        public int Id { get; set; }

        [Required]
        [StringLength(7, MinimumLength = 2,  ErrorMessage = "It must be between 2 to 7 characters")]
        public string Code { get; set; }

        [Required]
        public string Name { get; set; }
    }
}