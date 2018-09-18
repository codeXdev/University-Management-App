using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UniversityManagementApp.Models
{
    public class ClassroomAllocation
    {
        [Required]
        [Display(Name = "Course")]
        public int CourseId { get; set; }

        [Required]
        [Display(Name = "Room")]
        public int RoomId { get; set; }

        [Required]
        [Display(Name = "Day")]
        public int DayId { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }

        //[Required]
        //public DateTime FromTime { get; set; }

        //[Required]
        //public DateTime ToTime { get; set; }

        [Required]
        [Display(Name = "From Time")]
        public string FromTime { get; set; }

        //[Required]
        //[Display(Name = "Course")]
        //public string FromTimeTt { get; set; }

        [Required]
        [Display(Name = "To Time")]
        public string ToTimeHours { get; set; }

        //[Required]
        //[Display(Name = "Course")]
        //public string ToTimeTt { get; set; }
    }
}