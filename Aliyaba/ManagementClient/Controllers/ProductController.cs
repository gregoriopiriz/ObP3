using BussinesLogic.Helpers;
using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class ProductController : Controller
    {
        public ActionResult Home()
        {

            ViewBag.CurrentController = "Product";
            ViewBag.AddFormTitle = "Agregar un Producto";

            return View("_CRUD");
        }
        public ActionResult GetAddForm()
        {
            H_Category HC = H_Category.getInstance();

            List<SelectListItem> SelectItems = new List<SelectListItem>();

            foreach (DTO_Category DT in HC.GetCategories())
            {
                SelectListItem option = new SelectListItem();
                option.Text = DT.Name;
                option.Value = DT.ID.ToString();
                SelectItems.Add(option);
            }

            ViewBag.SelectItems = SelectItems;

            return PartialView("_AddForm");
        }
        public ActionResult GetEditForm()
        {
            H_Category HC = H_Category.getInstance();

            List<SelectListItem> SelectItems = new List<SelectListItem>();

            foreach (DTO_Category DT in HC.GetCategories())
            {
                SelectListItem option = new SelectListItem();
                option.Text = DT.Name;
                option.Value = DT.ID.ToString();
                SelectItems.Add(option);
            }

            ViewBag.SelectItems = SelectItems;

            return PartialView("_EditForm");
        }
        public ActionResult GetTableBody()
        {
            H_Category HC = H_Category.getInstance();

            H_Product HP = H_Product.getInstance();

            List<DTO_Product> Products = HP.GetAllProducts();

            foreach(DTO_Product P in Products)
            {
                P.CategoryName = HC.GetCategoryNameByID(int.Parse(P.CategoryID.ToString()));
            }

            ViewBag.TableList = Products;

            return PartialView("_TableBody");
        }

        public ActionResult AddProduct(DTO_Product P)
        {
            H_Product HP = H_Product.getInstance();

            HP.AddProduct(P);

            return RedirectToAction("Home");
        }
        public ActionResult EditProduct(DTO_Product P)
        {
            H_Product HP = H_Product.getInstance();

            HP.EditProduct(P);

            return RedirectToAction("Home");
        }
        public ActionResult GetImageByProductCode(string ProductCode)
        {
            H_Photo HP = H_Photo.getInstance();

            DTO_Photo _P = HP.GetImageByProductCode(ProductCode);

            return File(_P.Photo, "image/jpg");
        }
        public ActionResult Delete(string ProductCode)
        {
            H_Product HP = H_Product.getInstance();

            HP.DeleteProduct(ProductCode);

            return RedirectToAction("Home");
        }

    }
}