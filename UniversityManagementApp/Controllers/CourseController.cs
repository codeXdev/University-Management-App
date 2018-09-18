using System.Collections;
using System.Collections.Generic;
using System.Web.Mvc;
using UniversityManagementApp.Gateway;
using UniversityManagementApp.JsonModels;
using UniversityManagementApp.Models;
using UniversityManagementApp.ViewModels;

namespace UniversityManagementApp.Controllers
{
    public class CourseController : Controller
    {
        // GET: Course/Create
        public ViewResult Create()
        {
            CourseViewModel viewModel = new CourseViewModel
            {
                Departments = DepartmentGateway.GetAllDepartments(),
                Semesters = SemesterGateway.GetAllSemseters()
            };

            return View(viewModel);
        }

        //POST: Course/Create
        [HttpPost]
        public ViewResult Create(Course course)
        {
            CourseViewModel viewModel = new CourseViewModel
            {
                Departments = DepartmentGateway.GetAllDepartments(),
                Semesters = SemesterGateway.GetAllSemseters()
            };

            if (!ModelState.IsValid)
            {
                viewModel.Course = course;
                return View(viewModel);
            }

            int rowAffected = CourseGateway.Save(course);

            if (rowAffected > 0)
            {
                ViewBag.message = "Course Saved";
                viewModel.Course = new Course();
                return View(viewModel);
            }

            return View("~/Views/Shared/BadRequest.cshtml");
        }


        public ViewResult Assign()
        {
            CourseAssignmentViewModel viewModel = new CourseAssignmentViewModel
            {
                Departments = DepartmentGateway.GetAllDepartments()
            };
            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ViewResult Assign(CourseAssignment courseAssignment)
        {
            CourseAssignmentViewModel viewModel = new CourseAssignmentViewModel
            {
                Departments = DepartmentGateway.GetAllDepartments()
            };

            if (!ModelState.IsValid)
            {
                return View(viewModel);
            }

            int rowAffected = CourseGateway.Assign(courseAssignment);
            if (rowAffected > 0)
            {
                return View(viewModel);
            }

            return View("~/Views/Shared/BadRequest.cshtml");
        }

        [HttpGet]
        public JsonResult GetTeachersAndCoursesByDepartment(int departmentId)
        {
            if (departmentId <= 0)
                return Json("Department Id cannot be null or less than zero");

            IEnumerable<Teacher> teachers = TeacherGateway.GetTeachersByDepartmentId(departmentId);
            IEnumerable<Course> courses = CourseGateway.GetCoursesByDepartment(departmentId);

            return Json(new { teachers, courses }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult GetCourseById(int courseId)
        {
            Course course = CourseGateway.GetCourseById(courseId);

            if (course == null)
                return Json("Course was not found by this Id", JsonRequestBehavior.AllowGet);

            return Json(course, JsonRequestBehavior.AllowGet);

        }

        public ViewResult Statics()
        {
            ViewBag.Departments = DepartmentGateway.GetAllDepartments();
            return View();
        }

        [HttpGet]
        public JsonResult GetStatics(int departmentId)
        {
            return Json(CourseGateway.GetCourseStatics(departmentId), JsonRequestBehavior.AllowGet);
        }
    }
}