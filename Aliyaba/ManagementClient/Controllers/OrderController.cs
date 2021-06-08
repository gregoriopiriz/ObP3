using BussinesLogic.Helpers;
using Common.DTOs;
using DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace Client.Controllers
{
    public class OrderController : Controller
    {
        public ActionResult Index()
        {

            ViewBag.CurrentController = "Order";
            ViewBag.AddFormTitle = "";

            return View("_CRUD");
        }

        public string GetAddForm()
        {
            return "";
        }
        public ActionResult GetTableBody()
        {
            JavaScriptSerializer JSS = new JavaScriptSerializer();

            H_Order HO = H_Order.getInstance();
            H_OrdersDetail HOD = H_OrdersDetail.getInstance();
            H_State HS = H_State.getInstance();

            List<DTO_Order> Orders = HO.GetAllOrders();
            List<DTO_OrdersDetail> OrdersDetails = new List<DTO_OrdersDetail>();
            List<DTO_State> States = new List<DTO_State>();

            foreach (DTO_Order O in Orders)
            {
                List<DTO_OrdersDetail> aux = HOD.GetOrdersDetailByOrderID(O.ID);
                List<DTO_State> aux2 = HS.GetStatesByOrderID(O.ID);
                foreach (DTO_OrdersDetail od in aux)
                {
                    OrdersDetails.Add(od);
                } 
                foreach (DTO_State s in aux2)
                {
                    States.Add(s);
                }


            }

            ViewBag.TableList = Orders;
            ViewBag.TableListDetails = OrdersDetails;
            ViewBag.TableListDetails2 = States;

            return PartialView("_TableBody");
        }

        public void DeliverOrder(int OrderID)
        {
            DTO_State S = new DTO_State();
            S.EmployeeID = int.Parse(Session["UserID"].ToString());
            S.State = States.Delivered;
            S.OrderID = OrderID;
            S.Date = DateTime.Now;

            H_State HS = H_State.getInstance();

            HS.AddState(S);
        }
    }
}