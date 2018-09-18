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
    public class TeacherController : Controller
    {
        // GET: Teacher/Save
        public ViewResult Save()
        {
            TeacherViewModel viewModel = new TeacherViewModel
            {
                Designations = DesignationGateway.GetAllDesignations(),
                Departments = DepartmentGateway.GetAllDepartments()
            };

            ViewBag.msg = "";
            return View(viewModel);
        }

        // POST: Teacher/Save
        [HttpPost]
        public ActionResult Save(Teacher teacher)
        {
            TeacherViewModel viewModel = new TeacherViewModel
            {
                Designations = DesignationGateway.GetAllDesignations(),
                Departments = DepartmentGateway.GetAllDepartments()
            };

            if (!ModelState.IsValid)
            {
                viewModel.Teacher = teacher;
                return View(viewModel);
            }

            int rowAffected = TeacherGateway.Save(teacher);

            if (rowAffected > 0)
            {
                return Redirect("Save");
            }

            return View("~/Views/Shared/_BadRequest");
        }

        [HttpGet]
        public JsonResult GetTeacherCreditReport(int teacherId)
        {
            TeacherCreditReportJsonModel jsonModel = TeacherGateway.GetTeacherCreditReport(teacherId);

            return Json(jsonModel, JsonRequestBehavior.AllowGet);

        }
    }
}