using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.ViewModels
{
    public class TeacherViewModel
    {
        public Teacher Teacher { get; set; }
        public IEnumerable<Designation> Designations { get; set; }
        public IEnumerable<Department> Departments { get; set; }
    }
}