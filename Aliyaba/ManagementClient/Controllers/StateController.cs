using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Persistence;
using BussinesLogic.Helpers;

namespace Client.Controllers
{
    public class StateController : Controller
    {
        // GET: State
        public string GetCurrentStateNameByOrderID(int OrderID)
        {
            H_State HS = H_State.getInstance();
            return HS.GetCurrentStateNameByOrderID(OrderID);
        }
    }
}