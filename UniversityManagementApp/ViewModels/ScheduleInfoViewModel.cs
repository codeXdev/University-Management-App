using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.ViewModels
{
    public class ScheduleInfoViewModel
    {
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public IEnumerable<string> RoomInfo { get; set; }
    }
}