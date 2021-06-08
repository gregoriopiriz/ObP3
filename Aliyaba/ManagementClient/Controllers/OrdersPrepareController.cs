using BussinesLogic.Helpers;
using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class OrdersPrepareController : Controller
    {
        public ActionResult Index()
        {
            H_Order HO = H_Order.getInstance();
            H_OrdersDetail HOD = H_OrdersDetail.getInstance();

            List<DTO_Order> O = HO.GetPendingOrders();
            List<DTO_OrdersDetail> OD = new List<DTO_OrdersDetail>();

            foreach (DTO_Order Or in O)
            {
                foreach (DTO_OrdersDetail _OrD in HOD.GetOrdersDetailByOrderID(Or.ID))
                {
                    OD.Add(_OrD);
                }
            }

            ViewBag.OrdersCount = O.Count;
            ViewBag.Orders = O.OrderByDescending(o => o.IsExpress);
            ViewBag.OrdersDetails = OD;

            return View("SelectOrder");
        }
        public string GetProductNameToShowOrders(string _Code)
        {
            DTO_Product _P = new DTO_Product();

            H_Product HP = H_Product.getInstance();

            _P = HP.GetProductByCode(_Code);

            return _P.Name;
        }

        public ActionResult GetOrderToPrepare(int _ID)
        {
            H_OrdersDetail HOD = H_OrdersDetail.getInstance();
            H_State HS = H_State.getInstance();

            List<DTO_OrdersDetail> OD = new List<DTO_OrdersDetail>();
            List<DTO_OrderPrepareDetail> OPD = new List<DTO_OrderPrepareDetail>();

            DTO_State _S = new DTO_State();
            _S.Date = DateTime.Now;
            _S.EmployeeID = int.Parse(Session["UserID"].ToString());
            _S.State = States.Preparing;
            _S.Name = "En Preparación";
            _S.OrderID = _ID;
            HS.AddState(_S);

            foreach (DTO_OrderPrepareDetail _OPD in HOD.GetOrdersPrepareDetailByOrderID(_ID))
            {
                OPD.Add(_OPD);
            }

            OD = HOD.GetOrdersDetailByOrderID(_ID);

            ViewBag.OrdersDetail = OD;
            ViewBag.OrdersPrepareDetail = OPD.OrderBy(o => o.Location);

            return View("Prepare");
        }
        public ActionResult FinishPrepare(int _OrderID)
        {
            H_State HS = H_State.getInstance();
            H_ReservedStock HRS = H_ReservedStock.getInstance();

            DTO_State _S = new DTO_State();
            _S.EmployeeID = int.Parse(Session["UserID"].ToString());
            _S.Date = DateTime.Now;
            _S.Name = "Preparado";
            _S.State = States.Prepared;
            _S.OrderID = _OrderID;

            HS.AddState(_S);

            HRS.FinishPrepare(_OrderID);

            return RedirectToAction("Index");
        }
    }
}