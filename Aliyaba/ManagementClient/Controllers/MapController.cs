using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class MapController : Controller
    {
        // GET: Map
        public ActionResult GetMap()
        {
            return PartialView("_Map");
        }
    }
}