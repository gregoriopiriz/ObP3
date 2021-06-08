using BussinesLogic.Helpers;
using Common.DTOs;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Client.Controllers
{
    public class ImageController : Controller
    {
        public ActionResult Index()
        {


            return View("AddImage");
        }

        public ActionResult AddImage(HttpPostedFileBase upload, string ProductCode)
        {
            if (upload != null && ProductCode != null)
            {
                byte[] imageData = null;
                using (var image = new BinaryReader(upload.InputStream))
                {
                    imageData = image.ReadBytes(upload.ContentLength);
                }

                H_Photo HP = H_Photo.getInstance();

                DTO_Photo _P = new DTO_Photo();
                _P.Photo = imageData;
                _P.ProductCode = ProductCode;

                HP.AddPhoto(_P);
            }

            return RedirectToAction("Index");
        }
    }
}