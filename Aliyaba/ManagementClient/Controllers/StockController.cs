using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.DTOs;
using BussinesLogic.Helpers;
using DataAccess.Persistence;

namespace Client.Controllers
{
    public class StockController : Controller
    {
        public ActionResult Index()
        {
            List<string> TableHeaders = new List<string>();
            TableHeaders.Add("Código de Producto");
            TableHeaders.Add("Ubicación");
            TableHeaders.Add("Cantidad");
            TableHeaders.Add("Razón");
            TableHeaders.Add("Fecha");

            ViewBag.CurrentController = "Stock";
            ViewBag.TableHeaders = TableHeaders;
            ViewBag.AddFormTitle = "Agregar un Stock";

            return View("_CRUD");
        }

        public ActionResult GetAddForm()
        {
            return PartialView("_AddForm");
        }
        public ActionResult GetTableBody()
        {
            H_Stock HS = H_Stock.getInstance();
            List<DTO_Stock> Stocks = HS.GetStocks();

            ViewBag.TableList = Stocks;

            return PartialView("_TableBody");
        }


        public ActionResult AddStock(DTO_Stock _S)
        {

            _S.Date = DateTime.Now;

            H_Stock HS = H_Stock.getInstance();

            HS.AddStock(_S);

            return RedirectToAction("Index");
        }
        /*public ActionResult Delete(int ID)
        {
            H_Category HC = H_Category.getInstance();

            HC.DeleteCategory(ID);

            return RedirectToAction("Home");
        }*/
    }
}