using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Common.DTOs;
using BussinesLogic.Helpers;
using System.Web.Security;

namespace Client.Controllers
{
    public class UserController : Controller
    {
        public ActionResult Home()
        {
            return View();
        }

        public ActionResult SignIn()
        {
            if (User.Identity.IsAuthenticated == true)
            {
                return Redirect("/Home");
            }

            return View();
        }

        [HttpPost]
        public ActionResult SignIn(DTO_LogIn S)
        {
            H_User HU = new H_User();
            if (HU.CustomerLogIn(S))
            {
                FormsAuthentication.SetAuthCookie(S.UserName, false);
                Session["UserName"] = S.UserName;
                return Redirect("/Home");
            }
            return View();
        }
    }
}