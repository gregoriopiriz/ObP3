using BussinesLogic.Helpers;
using Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class DocumentTypeController : Controller
    {
        public ActionResult Home()
        {
            List<string> TableHeaders = new List<string>();
            TableHeaders.Add("Nombre");

            ViewBag.CurrentController = "DocumentType";
            ViewBag.TableHeaders = TableHeaders;
            ViewBag.AddFormTitle = "Agregar un Tipo de Documento";

            return View("_CRUD");
        }
        public ActionResult GetAddForm()
        {
            return PartialView("_AddForm");
        }
        public ActionResult GetTableBody()
        {
            H_DocumentType HDT = H_DocumentType.getInstance();

            List<DTO_DocumentType> DocumentTypes = HDT.GetDocumentTypes();

            ViewBag.TableList = DocumentTypes;

            return PartialView("_TableBody");
        }


        public ActionResult AddDocumentType(DTO_DocumentType _DT)
        {
            H_DocumentType HDT = H_DocumentType.getInstance();

            HDT.AddDocumentType(_DT);

            return RedirectToAction("Home");
        }
        public ActionResult Delete(string Name)
        {
            H_DocumentType HDT = H_DocumentType.getInstance();

            HDT.DeleteDocumentType(Name);

            return RedirectToAction("Home");
        }
        public ActionResult ConfirmDeleteDocumentType(string Name)
        {
            ViewBag.Name = Name;

            return PartialView("_ConfirmDeleteDocumentTypeButton");
        }
    }
}