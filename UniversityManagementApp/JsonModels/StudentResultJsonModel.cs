using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.JsonModels
{
    public class StudentResultJsonModel : StudentCourseEnrollmentJsonModel
    {
        public IEnumerable<Grade> Grades { get; set; }
    }
}