using BussinesLogic.Helpers;
using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Client.Controllers
{
    public class ReportsController : Controller
    {
        // GET: Reports
        public ActionResult Index()
        {
            return View("Reports");
        }
     
        public ActionResult MostSoldProduct()
        {
            H_Reports HR = H_Reports.getInstance();

            ViewBag.Products = HR.MostSoldProduct();

            return View();
        }

        public ActionResult ThermicMap()
        {

            H_Order HO = H_Order.getInstance();

            List<DTO_Location> Locations = HO.GetDeliveredOrderLocations();


            ViewBag.Locations = Locations;

            return PartialView();
        }

        public ActionResult OrdersAndItemsQuantityForm()
        {
            return PartialView("OrdersAndItemsQuantityForm");
        }

        public ActionResult OrdersAndItemsQuantity(DateTime Date)
        {
            H_Reports HR = H_Reports.getInstance();

            DTO_OrdersAndItemsQuantity OAIQ = HR.OrdersAndItemsQuantity(Date);

            ViewBag.OAIQ = OAIQ;
            ViewBag.Date = Date.ToString("dd/MM/yyyy");

            return View();
        }
        public ActionResult DeliveredAverageForm()
        {
            return PartialView();
        }
        public ActionResult DeliveredAverage(DateTime Date1, DateTime Date2)
        {
            H_Reports HR = H_Reports.getInstance();

            TimeSpan t = HR.DeliveredAverage(Date1, Date2);

            ViewBag.Average = t.ToString("%d") + " día(s), " + t.ToString("%h") + " hora(s), " + t.ToString("%m") + " minuto(s)";
            ViewBag.Date1 = Date1.ToString("dd/MM/yyyy");
            ViewBag.Date2 = Date2.ToString("dd/MM/yyyy");

            return View();
        }
    }
}