using BussinesLogic.Helpers;
using Common.DTOs;
using DataAccess;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace CustomersClient.Controllers
{
    public class ShoppingCartController : Controller
    {
        public ActionResult Index()
        {
            H_Location HL = H_Location.getInstance();
            H_Order HO = H_Order.getInstance();

            List<DTO_Location> Locations = new List<DTO_Location>();
            if (Session["UserName"] != null)
            {
                Locations = HL.GetLocationsByUser(Session["UserName"].ToString());
            }
            ViewBag.Locations = Locations;

            return View("ShoppingCart");
        }
        public void AddOrder(bool isExpress, float Total, int LocationID)
        {
            H_Order HO = H_Order.getInstance();
            H_State HS = H_State.getInstance();

            DTO_Order _O = new DTO_Order();

            _O.IsExpress = isExpress;
            _O.TotalPrice = Total;
            _O.EntryDate = DateTime.Now;
            _O.LocationID = LocationID;
            _O.CustomerUsername = Session["UserName"].ToString();

            HO.AddOrder(_O);

            DTO_Order _OO = new DTO_Order();
            _OO = HO.GetLastOrderByUserName(_O.CustomerUsername);

            Session["CurrentOrderID"] = _OO.ID;

            DTO_State _S = new DTO_State();

            _S.Date = DateTime.Now;
            _S.State = States.Pending;
            _S.OrderID = _OO.ID;

            HS.AddState(_S);
        }

        public void AddOrderDetail(int Quantity, string ProductCode)
        {
            DTO_OrdersDetail _OD = new DTO_OrdersDetail();
            _OD.OrderID = int.Parse(Session["CurrentOrderID"].ToString());
            _OD.Quantity = Quantity;
            _OD.ProductCode = ProductCode;

            H_OrdersDetail HOD = H_OrdersDetail.getInstance();

            HOD.AddOrdersDetail(_OD);
        }
    }
}