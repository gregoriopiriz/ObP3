using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using BussinesLogic.Helpers;
using Common.DTOs;

namespace Client.Controllers
{
    public class LocationController : Controller
    {
        public ActionResult Home()
        {
            List<string> TableHeaders = new List<string>();
            TableHeaders.Add("Latitud");
            TableHeaders.Add("Longitud");
            TableHeaders.Add("Nombre de Usuario");
            ViewBag.CurrentController = "Location";
            ViewBag.TableHeaders = TableHeaders;
            ViewBag.AddFormTitle = "Agregar una locación";
            return View("_CRUD");
        }
        public ActionResult GetAddForm()
        {
            return PartialView("_AddForm");
        }
        public ActionResult GetTableBody()
        {
            H_Location HL = H_Location.getInstance();

            List<DTO_Location> Locations = HL.GetLocations();

            ViewBag.TableList = Locations;

            return PartialView("_TableBody");
        }


        public ActionResult AddLocation(DTO_Location _L)
        {
            H_Location HL = H_Location.getInstance();

            HL.AddLocation(_L);

            return RedirectToAction("Home");
        }
        public ActionResult Delete(int ID)
        {
            H_Location HL = H_Location.getInstance();

            HL.DeleteLocation(ID);

            return RedirectToAction("Home");
        }
    }
}