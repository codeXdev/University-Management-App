using System.Collections.Generic;
using System.Web.Mvc;
using UniversityManagementApp.Gateway;
using UniversityManagementApp.Models;

namespace UniversityManagementApp.Controllers
{
    public class DepartmentController : Controller
    {
        // GET: Department/Save
        public ActionResult Save()
        {
            return View();
        }

        // POST: Department/Save
        [HttpPost]
        public ActionResult Save(Department department)
        {
            if (!ModelState.IsValid)
            {
                return View(department);
            }

            int rowAffected = DepartmentGateway.Save(department);

            if (rowAffected > 0)
            {
                return RedirectToAction("Save");
            }

            return HttpNotFound();
        }

        //GET: Deparment/DeprtmentList
        public ViewResult DepartmentsList()
        {
            IEnumerable<Department> departments = DepartmentGateway.GetAllDepartments();
            return View(departments);
        }
    }
}