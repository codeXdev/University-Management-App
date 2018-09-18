using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UniversityManagementApp.Gateway;
using UniversityManagementApp.JsonModels;
using UniversityManagementApp.Models;
using UniversityManagementApp.ViewModels;

namespace UniversityManagementApp.Controllers
{
    public class RoomController : Controller
    {
        // GET: Room/Allocate
        public ViewResult Allocate()
        {
            ClassroomAllocationViewModel viewModel = new ClassroomAllocationViewModel
            {
                Departments = DepartmentGateway.GetAllDepartments(),
                Days = DayGateway.GetAllDays(),
                Rooms = RoomGateway.GetAllRooms()
        };
            return View(viewModel);
        }

        // POST: Room/Allocate
        [HttpPost]
        public ViewResult Allocate(ClassroomAllocation classroomAllocation)
        {
            ClassroomAllocationViewModel viewModel = new ClassroomAllocationViewModel
            {
                Departments = DepartmentGateway.GetAllDepartments(),
                Days = DayGateway.GetAllDays(),
                Rooms = RoomGateway.GetAllRooms()
            };

            if(!ModelState.IsValid)
            {
                viewModel.ClassroomAllocation = classroomAllocation;
                ViewBag.Message = "Unsuccessful";
                return View(viewModel);
            }


            int rowAffected = RoomGateway.AllocateRoom(classroomAllocation);

            if (rowAffected > 0)
                ViewBag.Message = "Successful";
            else
                ViewBag.Message = "Not Saved";

            return View(viewModel);

        }

        [HttpGet]
        public JsonResult GetCoursesByDepartment(int departmentId)
        {
            IEnumerable<Course> courses = CourseGateway.GetCoursesByDepartment(departmentId);
            return Json(courses, JsonRequestBehavior.AllowGet);
        }

        public ViewResult AllocationInformation()
        {
            ViewBag.Departments = DepartmentGateway.GetAllDepartments();
            return View();
        }

        [HttpGet]
        public JsonResult AllocattionInformation()
        {
            return Json(null);
        }


        public ViewResult Schedules()
        {
            IEnumerable<ScheduleInfoViewModel> infos = RoomGateway.GetSchedule(1);
            return View(infos);
        }
    }
}