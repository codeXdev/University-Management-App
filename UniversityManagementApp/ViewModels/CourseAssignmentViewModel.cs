
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.ViewModels
{
    public class CourseAssignmentViewModel
    {
        public IEnumerable<Department> Departments{ get; set; }
        public CourseAssignment CourseAssignment { get; set; }
    }
}