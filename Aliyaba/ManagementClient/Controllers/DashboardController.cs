using BussinesLogic.Helpers;
using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Client.Controllers
{
    public class DashboardController : Controller
    {
        // GET: Dashboard
        public ActionResult Index()
        {
            if (Session["UserName"] != null)
            {
                return View("Dashboard");
            }
            else
            {
                return RedirectToAction("SignIn", "Dashboard");
            }
        }
        public ActionResult SignIn()
        {
            return View();
        }
        public ActionResult LogIn(DTO_LogIn LI)
        {
            H_User HU = H_User.getInstance();

            DTO_Employee _E = HU.EmployeeLogIn(LI);

            if (_E != null)
            {
                FormsAuthentication.SetAuthCookie(LI.UserName, true);
                Session["UserName"] = _E.UserName;
                Session["UserID"] = _E.ID;
                Session["UserPosition"] = _E.positionName;
                Session.Timeout = 600;
            }

            return RedirectToAction("Index", "Dashboard");
        }
        public ActionResult LogOut(DTO_LogIn LI)
        {

            Session.Clear();

            return RedirectToAction("Index", "Dashboard");
        }
    }
}