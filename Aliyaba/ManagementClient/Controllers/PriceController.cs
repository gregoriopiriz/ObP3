using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BussinesLogic.Helpers;
using Common.DTOs;

namespace Client.Controllers
{
    public class PriceController : Controller
    {
        public ActionResult Home()
        {

            List<string> TableHeaders = new List<string>();
            TableHeaders.Add("Fecha");
            TableHeaders.Add("Código del Producto");
            TableHeaders.Add("Precio");

            ViewBag.CurrentController = "Price";
            ViewBag.TableHeaders = TableHeaders;
            ViewBag.AddFormTitle = "Agregar un Precio";

            return View("_CRUD");
        }

        public ActionResult GetAddForm()
        {
            return PartialView("_AddForm");
        }

        public ActionResult GetTableBody()
        {
            H_Price HP = H_Price.getInstance();

            List<DTO_Price> Prices = HP.GetPrices();

            ViewBag.TableList = Prices;

            return PartialView("_TableBody");
        }

        public ActionResult AddPrice(DTO_Price _P)
        {
            H_Price HP = H_Price.getInstance();

            HP.AddPrice(_P);

            return RedirectToAction("Home");
        }
    }
}