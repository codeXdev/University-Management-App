using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using UniversityManagementApp.Gateway;
using UniversityManagementApp.JsonModels;
using UniversityManagementApp.Models;
using UniversityManagementApp.ViewModels;

namespace UniversityManagementApp.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        public ViewResult Register()
        {
            RegisterStudentViewModel viewModel = new RegisterStudentViewModel
            {
                Departments = DepartmentGateway.GetAllDepartments()
            };
            return View(viewModel);
        }

        // POST: Student
        [HttpPost]
        public ViewResult Register(Student student)
        {
            RegisterStudentViewModel viewModel = new RegisterStudentViewModel
            {
                Departments = DepartmentGateway.GetAllDepartments()
            };

            if (ModelState.IsValid)
            {
                StudentGateway sg = new StudentGateway();

                string registrationNumber = GetRegistrationNumber(student);

                int rowAffected = sg.Save(student, registrationNumber);

                if (rowAffected > 0)
                    ViewBag.Message = "Saved";
                else
                    ViewBag.Message = "Error";

                return View(viewModel);
            }

            // Model state is not valid
            viewModel.Student = student;

            return View(viewModel);
        }

        public string GetRegistrationNumber(Student student)
        {
            Department department = DepartmentGateway.GetDepartmentById(student.DepartmentId);
            int year = DateTime.Now.Year;
            int studentsNumber = StudentGateway.GetStudentNumberByDepartmentAndYear(department.Id, year);

            //Assigning the next number
            studentsNumber++;

            string numberOfStudents;

            if (studentsNumber < 10)
            {
                numberOfStudents = "00" + studentsNumber;
            }
            else if (studentsNumber < 100)
            {
                numberOfStudents = "0" + studentsNumber;
            }
            else
            {
                numberOfStudents = studentsNumber.ToString();
            }

            return department.Code + "-" + year + "-" + numberOfStudents;
        }


        /*
         * Course Enrollment
         */
        public ViewResult EnrollInCourse()
        {
            CourseEnrollmentViewModel viewModel = new CourseEnrollmentViewModel
            {
                Students = StudentGateway.GetStudents()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ViewResult EnrollInCourse(CourseEnrollment courseEnrollment)
        {
            CourseEnrollmentViewModel viewModel = new CourseEnrollmentViewModel
            {
                Students = StudentGateway.GetStudents()
            };

            if (!ModelState.IsValid)
            {
                viewModel.CourseEnrollment = courseEnrollment;

                ViewBag.Message = "Saved";

                return View(viewModel);
            }

            int rowAffected = StudentGateway.Enroll(courseEnrollment);

            if (rowAffected > 0)
                ViewBag.Message = "Saved";
            else
                ViewBag.Message = "Error";

            return View(viewModel);
        }


        [HttpGet]
        public JsonResult GetStudentInfo(int studentId)
        {
            StudentCourseEnrollmentJsonModel jsonModel = StudentGateway.GetCourseEnrollmentJsonModel(studentId);
            return Json(jsonModel, behavior: JsonRequestBehavior.AllowGet);
        }


        public ViewResult SaveResult()
        {
            ViewBag.Students = StudentGateway.GetStudents();
            return View();
        }


        [HttpPost]
        public ActionResult SaveResult(StudentResult studentResult)
        {
            ViewBag.Students = StudentGateway.GetStudents();

            if (!ModelState.IsValid)
            {
            ViewBag.Message = "Saved";
                return View(studentResult);
            }

            int rowAffected = StudentGateway.SaveResult(studentResult);

            if(rowAffected > 0)
                ViewBag.Message = "Saved";
            else
                ViewBag.Message = "Error";

            return View();
        }

        [HttpGet]
        public JsonResult GetStudentInfoForSaveResult(int studentId)
        {
            StudentResultJsonModel model = StudentGateway.GetSaveResultJsonModel(studentId);
            return Json(model, JsonRequestBehavior.AllowGet);
        }

        public ViewResult ViewResult()
        {
            ViewBag.Students = StudentGateway.GetStudents();
            return View();
        }
    }
}