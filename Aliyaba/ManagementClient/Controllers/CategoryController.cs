using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.DTOs;
using BussinesLogic.Helpers;
using DataAccess.Persistence;
using System.Web.Script.Serialization;

namespace Client.Controllers
{
    public class CategoryController : Controller
    {
        public ActionResult Home()
        {
            List<string> TableHeaders = new List<string>();
            TableHeaders.Add("Nombre");

            ViewBag.CurrentController = "Category";
            ViewBag.TableHeaders = TableHeaders;
            ViewBag.AddFormTitle = "Agregar una Categoría";

            return View("_CRUD");
        }
        public ActionResult GetAddForm()
        {
            return PartialView("_AddForm");
        }
        public ActionResult GetTableBody()
        {
            JavaScriptSerializer JSS = new JavaScriptSerializer();

            H_Category HC = H_Category.getInstance();

            List<DTO_Category> Categories = HC.GetCategories();

            ViewBag.TableList = Categories;

            return PartialView("_TableBody");
        }


        public ActionResult AddCategory(DTO_Category _C)
        {
            H_Category HC = H_Category.getInstance();

            HC.AddCategory(_C);

            return RedirectToAction("Home");
        }
        public ActionResult Delete(int ID)
        {
            H_Category HC = H_Category.getInstance();

            HC.DeleteCategory(ID);

            return RedirectToAction("Home");
        }
        public ActionResult ConfirmDelete(int ID)
        {
            ViewBag.ID = ID;

            return PartialView("_ConfirmDeleteCategoryButton");
        }


    }
}