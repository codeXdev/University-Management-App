using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using UniversityManagementApp.JsonModels;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.Gateway
{
    public class CourseGateway : Gateway
    {
        public static int Save(Course course)
        {
            string sql = "INSERT INTO Courses (Code, Name, Credit, Description, DepartmentId, SemesterId) " +
                         "VALUES (@code, @name, @credit, @description, @departmentId, @semesterId)";

            Command = new SqlCommand(sql, Connection);

            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("code", course.Code);
            Command.Parameters.AddWithValue("name", course.Name);
            Command.Parameters.AddWithValue("credit", course.Credit);

            if (course.Description == null)
                Command.Parameters.AddWithValue("description", DBNull.Value);
            else
                Command.Parameters.AddWithValue("description", course.Description);

            Command.Parameters.AddWithValue("departmentId", course.DepartmentId);
            Command.Parameters.AddWithValue("semesterId", course.SemesterId);

            Connection.Open();

            int rowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return rowAffected;
        }


        //All the departments
        public static IList<Course> GetAllCourses()
        {
            List<Course> courses = null;

            string sql = "SELECT * FROM Courses";

            Command = new SqlCommand(sql, Connection);

            Connection.Open();

            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                courses = new List<Course>();
                while (reader.Read())
                {
                    Course course = new Course
                    {
                        Id = (int)reader["Id"],
                        Code = reader["Code"].ToString(),
                        Name = reader["Name"].ToString(),
                        Credit = (double)reader["Credit"],
                        Description = reader["Description"].ToString(),
                        DepartmentId = (int)reader["DepartmentId"],
                        SemesterId = (int)reader["SemesterId"]
                    };

                    courses.Add(course);
                }
            }

            Connection.Close();

            return courses;
        }


        //All the departments
        public static IList<Course> GetCoursesByDepartment(int departmentId)
        {
            List<Course> courses = null;

            string sql = "SELECT * FROM Courses WHERE DepartmentId = " + departmentId;

            Command = new SqlCommand(sql, Connection);

            Connection.Open();

            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                courses = new List<Course>();
                while (reader.Read())
                {
                    Course course = new Course
                    {
                        Id = (int)reader["Id"],
                        Code = reader["Code"].ToString(),
                        Name = reader["Name"].ToString(),
                        Credit = (double)reader["Credit"],
                    };

                    courses.Add(course);
                }
            }

            Connection.Close();

            return courses;
        }

        //All the departments
        public static Course GetCourseById(int courseId)
        {
            Course course = null;

            string sql = "SELECT * FROM Courses WHERE Id = " + courseId;

            Command = new SqlCommand(sql, Connection);

            Connection.Open();

            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                course = new Course
                {
                    Id = (int)reader["Id"],
                    Code = reader["Code"].ToString(),
                    Name = reader["Name"].ToString(),
                    Credit = (double)reader["Credit"],
                    Description = reader["Description"].ToString(),
                    DepartmentId = (int)reader["DepartmentId"],
                    SemesterId = (int)reader["SemesterId"]
                };
            }

            Connection.Close();

            return course;
        }


        public static int Assign(CourseAssignment assignment)
        {
            string sql = "INSERT INTO CourseAssignments (TeacherId, CourseId) VALUES (@teacherId, @courseId)";

            Command = new SqlCommand(sql, Connection);

            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("teacherId", assignment.TeacherId);
            Command.Parameters.AddWithValue("courseId", assignment.CourseId);

            Connection.Open();

            int rowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return rowAffected;
        }

        public static IEnumerable<CourseStaticsJsonModel> GetCourseStatics(int departmentId)
        {
            List<CourseStaticsJsonModel> courseStatics = null;

            string sql = "SELECT * FROM CourseStaticsView WHERE DepartmentId =" + departmentId;

            Command = new SqlCommand(sql, Connection);

            Connection.Open();

            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                courseStatics = new List<CourseStaticsJsonModel>();
                while (reader.Read())
                {
                    CourseStaticsJsonModel courseStatic = new CourseStaticsJsonModel()
                    {
                        Code = reader["Code"].ToString(),
                        Name = reader["Name"].ToString(),
                        Semester = reader["Semester"].ToString(),
                        AssignedTo = reader["AssignedTo"].ToString()
                    };

                    courseStatics.Add(courseStatic);
                }
            }

            Connection.Close();

            return courseStatics;
        }


        public static IEnumerable<Course> GetCoursesByEnrollemt(int studentId)
        {
            List<Course> courses = null;

            string sql = "SELECT c.Id, c.Name FROM CourseEnrollments ca JOIN Courses c ON c.Id = ca.CourseId WHERE ca.StudentId =" + studentId;

            Command = new SqlCommand(sql, Connection);

            Connection.Open();

            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                courses = new List<Course>();
                while (reader.Read())
                {
                    Course course = new Course
                    {
                        Id = (int)reader["Id"],
                        Name = reader["Name"].ToString(),
                    };

                    courses.Add(course);
                }
            }

            Connection.Close();

            return courses;
        }
    }
}