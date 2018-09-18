using System;

namespace UniversityManagementApp.ViewModels
{
    public class RoomInfoViewModel
    {
        public string RoomNo { get; set; }
        public string Day { get; set; }
        public DateTime FromTime { get; set; }
        public DateTime ToTime { get; set; }

        //public string GetRoomInfo()
        //{
        //    return "R. No : " + RoomNo + ", " + Day + ", " + FromTime.ToString("hh:mm tt") + " - " + ToTime.ToString("hh:mm tt");
        //}
    }
}