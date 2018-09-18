using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.JsonModels
{
    public class ViewResultJsonModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string DepartmentName { get; set; }
        public IEnumerable<CourseGradeJsonModel> CourseGrade { get; set; }
    }
}