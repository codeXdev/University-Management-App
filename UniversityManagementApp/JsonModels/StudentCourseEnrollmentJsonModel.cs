using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.JsonModels
{
    public class StudentCourseEnrollmentJsonModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}