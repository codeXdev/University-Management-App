using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.ViewModels
{
    public class ClassroomAllocationViewModel
    {
        public ClassroomAllocation ClassroomAllocation { get; set; }
        public IEnumerable<Department> Departments { get; set; }
        public IEnumerable<Day> Days { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
    }
}