using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.ViewModels
{
    public class RegisterStudentViewModel
    {
        public Student Student { get; set; }
        public IEnumerable<Department> Departments { get; set; }
    }
}