using BussinesLogic.Helpers;
using Common.DTOs;
using DataAccess;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace CustomersClient.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Home()
        {
            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("SignIn");
            }
        }
        public ActionResult SignIn()
        {
            return View();
        }
        public ActionResult SignUp()
        {


            return View();
        }

        public ActionResult Register(DTO_Customer C)
        {
            H_Customer HC = H_Customer.getInstance();

            HC.AddCustomer(C);

            return RedirectToAction("Index", "Shop");
        }

        public ActionResult LogIn(DTO_LogIn LI)
        {
            H_User HU = H_User.getInstance();

            if (HU.CustomerLogIn(LI))
            {
                FormsAuthentication.SetAuthCookie(LI.UserName, true);
                Session["UserName"] = LI.UserName;
                Session.Timeout = 60;
            }

            return RedirectToAction("Index", "Shop");
        }
        public ActionResult LogOut(DTO_LogIn LI)
        {

            Session.Clear();

            return RedirectToAction("Index", "Shop");
        }

        public ActionResult Locations()
        {
            H_Location HL = H_Location.getInstance();

            List<DTO_Location> DTO_Locations = HL.GetLocationsByUser(Session["UserName"].ToString());

            ViewBag.Locations = DTO_Locations;

            return View();
        }

        public string GetLocationsByUser()
        {
            H_Location HL = H_Location.getInstance();

            JavaScriptSerializer JSS = new JavaScriptSerializer();

            List<DTO_Location> DTO_Locations = HL.GetLocationsByUser(Session["UserName"].ToString());

            return JSS.Serialize(DTO_Locations);
        }
        public ActionResult DeleteLocation(int ID)
        {
            H_Location HL = H_Location.getInstance();

            HL.DeleteLocation(ID);

            return RedirectToAction("Locations");
        }

        public ActionResult GetTable()
        {
            return PartialView("_LocationsTable");
        }
        public ActionResult GetAddForm()
        {
            return PartialView("_LocationAddForm");
        }

        public ActionResult AddLocation(DTO_Location L)
        {
            H_Location HL = H_Location.getInstance();

            HL.AddLocation(L);

            return RedirectToAction("Locations");
        }
        public string GetLocation(int ID)
        {
            H_Location HL = H_Location.getInstance();

            DTO_Location L = HL.GetLocationByID(ID);

            JavaScriptSerializer JSS = new JavaScriptSerializer();

            return JSS.Serialize(L);
        }

        public ActionResult Orders()
        {
            H_Order HO = H_Order.getInstance();
            H_OrdersDetail HOD = H_OrdersDetail.getInstance();

            List<DTO_Order> O = HO.GetOrdersByUserName(Session["UserName"].ToString());
            List<DTO_OrdersDetail> OD = new List<DTO_OrdersDetail>();

            foreach (DTO_Order Or in O)
            {
                foreach (DTO_OrdersDetail _OrD in HOD.GetOrdersDetailByOrderID(Or.ID))
                {
                    OD.Add(_OrD);
                }
            }

            ViewBag.Orders = O;
            ViewBag.OrdersDetails = OD;

            return View("Orders");
        }

        public string GetProductNameToShowOrders(string _Code)
        {
            DTO_Product _P = new DTO_Product();

            H_Product HP = H_Product.getInstance();

            _P = HP.GetProductByCode(_Code);

            return _P.Name;
        }
        public string GetProductPriceToShowOrders(string _Code)
        {
            DTO_Product _P = new DTO_Product();

            H_Product HP = H_Product.getInstance();

            _P = HP.GetProductByCode(_Code);

            return _P.Price.Price.ToString();
        }

        public ActionResult CancelOrder(int ID)
        {
            H_Order HO = H_Order.getInstance();

            HO.CancelOrder(ID);

            return RedirectToAction("Orders");
        }

        public ActionResult GetSignInForm()
        {
            return PartialView("SignIn");
        }
        public ActionResult GetSignUpForm()
        {
            H_DocumentType HDT = H_DocumentType.getInstance();

            List<SelectListItem> SelectItems = new List<SelectListItem>();

            foreach (DTO_DocumentType DT in HDT.GetDocumentTypes())
            {
                SelectListItem option = new SelectListItem();
                option.Text = DT.Name;
                option.Value = DT.Name;
                SelectItems.Add(option);
            }

            ViewBag.DocumentTypes = SelectItems;

            return PartialView("SignUp");
        }

        public JsonResult checkUserName(string UserName)
        {
            bool rest = true;
            using (AliyabaEntities context = new AliyabaEntities())
            {
                if (context.Employees.Any(a => a.UserName == UserName))
                {
                    rest = false;
                }
            }
            return Json(rest, JsonRequestBehavior.AllowGet);
        }
    }
}