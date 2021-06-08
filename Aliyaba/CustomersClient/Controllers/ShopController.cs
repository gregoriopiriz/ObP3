using BussinesLogic.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataAccess.Mappers;
using Common.DTOs;
using DataAccess;
using System.IO;

namespace CustomersClient.Controllers
{
    public class ShopController : Controller
    {
        public ActionResult Index()
        {
            H_Product HP = H_Product.getInstance();
            H_Category HC = H_Category.getInstance();

            ViewBag.Categories = HC.GetCategories();
            ViewBag.Products = HP.GetProducts();
            return View("Products");
        }

        public ActionResult GetImageByProductCode(string ProductCode)
        {
            H_Photo HP = H_Photo.getInstance();

            DTO_Photo _P = HP.GetImageByProductCode(ProductCode);

            return File(_P.Photo, "image/jpg");
        }

        public int GetStockByProductCode(string ProductCode)
        {
            H_Stock HS = H_Stock.getInstance();

            int _S = HS.GetQuantityOfStockByProductCode(ProductCode);

            return _S;
        } 
    }
}