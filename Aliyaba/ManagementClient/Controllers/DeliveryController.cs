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
    public class DeliveryController : Controller
    {
        public ActionResult Index()
        {

            ViewBag.CurrentController = "Delivery";
            ViewBag.AddFormTitle = "Agregar una Categoría";

            return View("_CRUD");
        }
        public ActionResult Prepare()
        {
            H_Order HO = H_Order.getInstance();
            H_OrdersDetail HOD = H_OrdersDetail.getInstance();

            List<DTO_Order> O = HO.GetToDeliverOrders();

            ViewBag.OrdersCount = O.Count;
            ViewBag.Orders = O.OrderByDescending(o => o.IsExpress);
            ViewBag.Locations = null;

            return View("SelectOrder");
        }
        public string GetAddForm()
        {
            return "";
        }
        public ActionResult GetTableBody()
        {
            H_Delivery HD = H_Delivery.getInstance();
            H_Order HO = H_Order.getInstance();

            List<DTO_Delivery> Deliveries = HD.GetAllDeliveries();
            List<DTO_Order> Orders = HO.GetDeliveredOrders();

            ViewBag.TableList = Deliveries;
            ViewBag.TableListDetails = Orders;

            return PartialView("_TableBody");
        }


        public void AddDeliveryIdToOrder(int _OrderID, int _DeliveryID)
        {
            H_Order HO = H_Order.getInstance();

            HO.AddDeliveryIdToOrder(_OrderID, _DeliveryID, int.Parse(Session["UserID"].ToString()));
        }

        public ActionResult AddDelivery(string _OrdersID, string VehiclePlate)
        {
            JavaScriptSerializer JSS = new JavaScriptSerializer();
            List<string> OrdersID = JSS.Deserialize<List<string>>(_OrdersID);

            DateTime date = DateTime.Now;

            H_Delivery HD = H_Delivery.getInstance();
            H_State HS = H_State.getInstance();

            DTO_Delivery _D = new DTO_Delivery();
            _D.CommitDate = date;
            _D.VehiclePlate = VehiclePlate;
            _D.EmployeeID = int.Parse(Session["UserID"].ToString());


            HD.AddDelivery(_D);

            int IdDelivery = HD.GetIdDelivery(int.Parse(Session["UserID"].ToString()), date);

            foreach (string ord in OrdersID)
            {
                DTO_State _S = new DTO_State();
                _S.Date = DateTime.Now;
                _S.EmployeeID = int.Parse(Session["UserID"].ToString());
                _S.State = States.ReadyForDelivery;
                _S.Name = "Listo para Entregar";
                _S.OrderID = int.Parse(ord);
                HS.AddState(_S);

                AddDeliveryIdToOrder(int.Parse(ord), IdDelivery);
            }
            

            return RedirectToAction("Prepare");
        }
        public void StartDelivery(int idD)
        {
            JavaScriptSerializer JSS = new JavaScriptSerializer();

            H_Delivery HD = H_Delivery.getInstance();
            H_Order HO = H_Order.getInstance();

            List<DTO_Order> Orders = HO.GetOrdersByDeliveryID(idD);

            H_Location HL = H_Location.getInstance();

            List<DTO_Location> DTO_Locations = HL.GetLocationsByDeliveryID(idD);


            string Locs = JSS.Serialize(DTO_Locations);
            string Ords = JSS.Serialize(Orders);

            Session["Locations"] = Locs;
            Session["Orders"] = Ords;
        }

        public ActionResult DeliveryMap()
        {
            JavaScriptSerializer JSS = new JavaScriptSerializer();

            List<DTO_Location> Locations = JSS.Deserialize<List<DTO_Location>>(Session["Locations"].ToString());

            List<DTO_Order> Orders = JSS.Deserialize<List<DTO_Order>>(Session["Orders"].ToString());

            ViewBag.Locations = Locations;
            ViewBag.Orders = Orders;

            return View("DeliveryMap");
        }

        public ActionResult SelectDelivery()
        {
            H_Delivery HD = H_Delivery.getInstance();

            List<DTO_Delivery> O = HD.GetDeliveriesToDeliver();

            ViewBag.Deliveries = O.OrderByDescending(o => o.CommitDate);
            ViewBag.DeliveriesCount = O.Count;
            return View("SelectDelivery");
        }
    }
}

