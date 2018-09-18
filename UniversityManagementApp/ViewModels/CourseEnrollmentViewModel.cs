using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.ViewModels
{
    public class CourseEnrollmentViewModel
    {
        public CourseEnrollment CourseEnrollment { get; set; }
        public IEnumerable<Student> Students { get; set; }
    }
}