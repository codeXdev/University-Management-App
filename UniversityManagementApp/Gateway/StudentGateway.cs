using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using UniversityManagementApp.JsonModels;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.Gateway
{
    public class StudentGateway : Gateway
    {
        public int Save(Student student, string regNo)
        {
            string sql = "INSERT INTO Students (Name, Email, ContactNo, Date, Address, DepartmentId, RegistrationNo) " +
                                        "VALUES (@name, @email, @contactNo, @date, @address, @departmentId, @regNo)";

            Command = new SqlCommand(sql, Connection);

            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("name", student.Name);
            Command.Parameters.AddWithValue("email", student.Email);
            Command.Parameters.AddWithValue("contactNo", student.ContactNo);
            Command.Parameters.AddWithValue("date", student.Date);

            if (student.Address != null)
                Command.Parameters.AddWithValue("address", student.Address);
            else
                Command.Parameters.AddWithValue("address", DBNull.Value);

            Command.Parameters.AddWithValue("departmentId", student.DepartmentId);
            Command.Parameters.AddWithValue("regNo", regNo);

            Connection.Open();

            int rowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return rowAffected;
        }

        public static int GetStudentNumberByDepartmentAndYear(int departmentId, int year)
        {
            int number = 0;
            string sql = "SELECT COUNT(Id) AS Number FROM Students " +
                         "WHERE YEAR(Date)=@year " +
                         "AND DepartmentId =@departmentId";


            Command = new SqlCommand(sql, Connection);
            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("year", year);
            Command.Parameters.AddWithValue("departmentId", departmentId);

            Connection.Open();

            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                number = (int)reader["Number"];
                reader.Close();
            }

            Connection.Close();

            return number;
        }


        public static IEnumerable<Student> GetStudents()
        {
            List<Student> students = null;
            string sql = "SELECT * FROM Students";

            Command = new SqlCommand(sql, Connection);

            Connection.Open();

            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                students = new List<Student>();
                while (reader.Read())
                {
                    Student student = new Student
                    {
                        Id = (int)reader["Id"],
                        RegistrationNo = reader["RegistrationNo"].ToString()
                    };
                    students.Add(student);
                }
                
            }
            reader.Close();
            Connection.Close();

            return students;
        }

        public static Student GetStudent(int studentId)
        {
            Student student = null;
            string sql = "SELECT * FROM Students WHERE Id=@studentId";

            Command = new SqlCommand(sql, Connection);

            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("studentId", studentId);

            Connection.Open();

            SqlDataReader reader = Command.ExecuteReader();

            if (reader.HasRows)
            {
                reader.Read();
                student = new Student
                {
                    Id = (int)reader["Id"],
                    Name = reader["Name"].ToString(),
                    Email = reader["Email"].ToString(),
                    DepartmentId = (int)reader["DepartmentId"]
                };                
            }
            reader.Close();
            Connection.Close();

            return student;
        }



        public static StudentCourseEnrollmentJsonModel GetCourseEnrollmentJsonModel(int studentId)
        {
            Student student = GetStudent(studentId);
            
            if(student == null)
                return null;

            Department department = DepartmentGateway.GetDepartmentById(student.DepartmentId);

            if (department == null)
                return null;

            IEnumerable<Course> courses = CourseGateway.GetCoursesByDepartment(student.DepartmentId);

            if (courses == null)
                return null;


            StudentCourseEnrollmentJsonModel model = new StudentCourseEnrollmentJsonModel
            {
                Name = student.Name,
                Email = student.Email,
                DepartmentName = department.Name,
                Courses = courses
            };

            return model;
        }


        public static int Enroll(CourseEnrollment courseEnrollment)
        {
            string sql = "INSERT INTO CourseEnrollments (StudentId, CourseId, EnrollmentDate) " +
                         "VALUES (@studentId, @courseId, @enrollmentDate)";

            Command = new SqlCommand(sql, Connection);

            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("studentId", courseEnrollment.StudentId);
            Command.Parameters.AddWithValue("courseId", courseEnrollment.CourseId);
            Command.Parameters.AddWithValue("enrollmentDate", courseEnrollment.Date);

            Connection.Open();

            int rowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return rowAffected;
        }


        public static int SaveResult(StudentResult studentResult)
        {
            string sql = "INSERT INTO Results (StudentId, CourseId, GradeId) " +
                         "VALUES (@studentId, @courseId, @gradeId)";

            Command = new SqlCommand(sql, Connection);

            Command.Parameters.Clear();
            Command.Parameters.AddWithValue("studentId", studentResult.StudentId);
            Command.Parameters.AddWithValue("courseId", studentResult.CourseId);
            Command.Parameters.AddWithValue("gradeId", studentResult.GradeId);

            Connection.Open();

            int rowAffected = Command.ExecuteNonQuery();

            Connection.Close();

            return rowAffected;
        }





        public static StudentResultJsonModel GetSaveResultJsonModel(int studentId)
        {
            Student student = GetStudent(studentId);

            if (student == null)
                return null;

            Department department = DepartmentGateway.GetDepartmentById(student.DepartmentId);

            if (department == null)
                return null;

            IEnumerable<Course> courses = CourseGateway.GetCoursesByEnrollemt(studentId);

            if (courses == null)
                return null;


            StudentResultJsonModel model = new StudentResultJsonModel
            {
                Name = student.Name,
                Email = student.Email,
                DepartmentName = department.Name,
                Courses = courses,
                Grades = GradeGateway.GetGrades()
            };

            return model;
        }
    }
}