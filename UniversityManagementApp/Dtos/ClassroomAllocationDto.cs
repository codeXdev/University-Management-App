using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Dtos
{
    public class ClassroomAllocationDto
    {
        [Required]
        public int CourseId { get; set; }

        [Required]
        public int RoomId { get; set; }

        [Required]
        public int DayId { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string FromTimeHours { get; set; }

        [Required]
        public string FromTimeTt { get; set; }

        [Required]
        public string ToTimeHours { get; set; }

        [Required]
        public string ToTimeTt { get; set; }
    }
}