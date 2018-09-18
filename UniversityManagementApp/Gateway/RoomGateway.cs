using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UniversityManagementApp.Models;
using UniversityManagementApp.ViewModels;

namespace UniversityManagementApp.Gateway
{
    public class RoomGateway : Gateway
    {
        //All the rooms
        public static IEnumerable<Room> GetAllRooms()
        {
            List<Room> rooms = null;

            string sql = "SELECT * FROM Rooms";

            Command = new SqlCommand(sql, Connection);

            Connection.Open();

            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                rooms = new List<Room>();
                while (reader.Read())
                {
                    Room room = new Room
                    {
                        Id = (int)reader["Id"],
                        Number = (int)reader["Number"]
                    };

                    rooms.Add(room);
                }
            }

            Connection.Close();

            return rooms;
        }


        public static int AllocateRoom(ClassroomAllocation classroomAllocation)
        {
            string sql = "INSERT INTO RoomAllocations (CourseId, RoomId, DayId, FromTime, ToTime) " +
                         "VALUES (@courseId, @roomId, @dayId, @fromTime, @toTime)";

            Command = new SqlCommand(sql, Connection);

            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("courseId", classroomAllocation.CourseId);
            Command.Parameters.AddWithValue("roomId", classroomAllocation.RoomId);
            Command.Parameters.AddWithValue("dayId", classroomAllocation.DayId);
            Command.Parameters.AddWithValue("fromTime", classroomAllocation.FromTime);
            Command.Parameters.AddWithValue("toTime", classroomAllocation.ToTimeHours);

            Connection.Open();

            int rowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return rowAffected;
        }

        public static IEnumerable<ScheduleInfoViewModel> GetSchedule(int departmentId)
        {
            IEnumerable<Course> courses = CourseGateway.GetCoursesByDepartment(departmentId);

            List<ScheduleInfoViewModel> schedules = null;

            ScheduleInfoViewModel schedule = null;

            if (courses != null)
            {
                schedules = new List<ScheduleInfoViewModel>();

                foreach (Course course in courses)
                {
                    schedule = new ScheduleInfoViewModel
                    {
                        CourseCode = course.Code,
                        CourseName = course.Name,
                        RoomInfo = GetRoomInfoByCourseId(course.Id)
                    };
                    schedules.Add(schedule);
                }

            }

            return schedules;
        }

        public static IEnumerable<string> GetRoomInfoByCourseId(int courseId)
        {
            List<string> infos = null;

            string sql = "SELECT r.Number, d.Name, ra.FromTime, ra.ToTime FROM RoomAllocations ra " +
                        "INNER JOIN Rooms r ON r.Id = ra.RoomId " +
                        "INNER JOIN DaysOfWeek d ON d.Id = ra.DayId " +
                        "WHERE ra.CourseId=" + courseId;


            Command = new SqlCommand(sql, Connection);

            Connection.Open();

            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                infos = new List<string>();
                while (reader.Read())
                {
                    RoomInfoViewModel model = new RoomInfoViewModel
                    {
                        RoomNo = reader["Number"].ToString(),
                        Day = reader["Name"].ToString(),
                        FromTime = DateTime.Parse(reader["FromTime"].ToString()),
                        ToTime = DateTime.Parse(reader["ToTime"].ToString())
                    };

                    infos.Add(GetRoomInfo(model));
                }
            }

            reader.Close();

            Connection.Close();

            return infos ?? new List<string> { "Not Allocated Yet Gateway" };
        }


        public static string GetRoomInfo(RoomInfoViewModel model)
        {
            return "R. No : " + model.RoomNo + ", " + model.Day + ", "
                   + model.FromTime.ToString("hh:mm tt") + " - "
                   + model.ToTime.ToString("hh:mm tt");

        }
    }
}