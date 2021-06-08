using BussinesLogic.Helpers;
using Common.DTOs;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Client.Controllers
{
    public class EmployeeController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.CurrentController = "Employee";
            ViewBag.AddFormTitle = "Agregar un Empleado";

            return View("_CRUD");
        }
        public ActionResult GetAddForm()
        {
            H_Position HP = H_Position.getInstance();

            List<SelectListItem> SelectItems = new List<SelectListItem>();

            foreach (DTO_Position P in HP.GetPositions())
            {
                SelectListItem option = new SelectListItem();
                option.Text = P.Name.ToUpper().Substring(0, 1) + P.Name.Substring(1, P.Name.Length - 1);
                option.Value = P.Name;
                SelectItems.Add(option);
            }

            ViewBag.Positions = SelectItems;

            return PartialView("_AddForm");
        }
        public ActionResult GetEditForm()
        {
            H_Position HP = H_Position.getInstance();

            List<SelectListItem> SelectItems = new List<SelectListItem>();

            foreach (DTO_Position P in HP.GetPositions())
            {
                SelectListItem option = new SelectListItem();
                option.Text = P.Name.ToUpper().Substring(0, 1) + P.Name.Substring(1, P.Name.Length - 1);
                option.Value = P.Name;
                SelectItems.Add(option);
            }

            ViewBag.Positions = SelectItems;

            return PartialView("_EditForm");
        }
        public ActionResult GetTableBody()
        {
            H_Employee HE = H_Employee.getInstance();

            List<DTO_Employee> Employees = HE.GetEmployees();

            ViewBag.TableList = Employees;

            return PartialView("_TableBody");
        }

        public ActionResult AddEmployee(DTO_Employee E)
        {
            H_Employee HE = H_Employee.getInstance();

            HE.AddEmployee(E);

            return RedirectToAction("Index");
        }
        public ActionResult Delete(string UserName)
        {
            H_Employee HE = H_Employee.getInstance();

            HE.DeleteEmployee(UserName);

            return RedirectToAction("Index");
        }

        public ActionResult EditEmployee(DTO_Employee E)
        {
            H_Employee HE = H_Employee.getInstance();

            HE.EditEmployee(E);

            return RedirectToAction("Index");
        }

        public JsonResult checkUserName(string UserName)
        {
            bool rest = true;
            using (AliyabaEntities context = new AliyabaEntities())
            {
                if (context.Employees.Any(a => a.UserName == UserName))
                {
                    rest = false;
                }
            }
            return Json(rest, JsonRequestBehavior.AllowGet);
        }
    }
}