using BussinesLogic.Helpers;
using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class CustomerController : Controller
    {
        public ActionResult Home()
        {

            List<string> TableHeaders = new List<string>();
            TableHeaders.Add("Usuario");
            TableHeaders.Add("Contraseña");
            TableHeaders.Add("Nombre");
            TableHeaders.Add("Apellido");
            TableHeaders.Add("Tipo de Documento");
            TableHeaders.Add("Documento");
            TableHeaders.Add("Número de Teléfono");
            TableHeaders.Add("Correo Electrónico");

            ViewBag.CurrentController = "Customer";
            ViewBag.TableHeaders = TableHeaders;
            ViewBag.AddFormTitle = "Agregar un Cliente";

            return View("_CRUD");
        }
        public ActionResult GetAddForm()
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

            return PartialView("_AddForm");
        }
        public ActionResult GetTableBody()
        {
            H_Customer HC = H_Customer.getInstance();

            List<DTO_Customer> Customers = HC.GetCustomers();

            ViewBag.TableList = Customers;

            return PartialView("_TableBody");
        }
        public ActionResult AddCustomer(DTO_Customer _C)
        {
            H_Customer HC = H_Customer.getInstance();

            HC.AddCustomer(_C);

            return RedirectToAction("Home");
        }

        public ActionResult Delete(string Document, string DocumentTypeName)
        {
            H_Customer HC = H_Customer.getInstance();

            HC.DeleteCustomer(Document, DocumentTypeName);

            return RedirectToAction("Home");
        }

        public ActionResult ConfirmDeleteCustomer(string Document, string DocumentTypeName)
        {
            ViewBag.Document = Document;
            ViewBag.DocumentTypeName = DocumentTypeName;

            return PartialView("_ConfirmDeleteCustomerButton");
        }
    }
}