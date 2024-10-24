using e_commerce.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_commerce.Controllers
{
    public class ProductController : Controller
    {

        private readonly ProductDataAccess _productdataAccess;
        private readonly CategoryDataAccess _categorydataaccess;
        public ProductController() {
            _productdataAccess = new ProductDataAccess();
            _categorydataaccess = new CategoryDataAccess();
        
        }

       

        // GET: Product
        public ActionResult Index()
        {
            var products = _productdataAccess.ListProduct();
            var categories = _categorydataaccess.CategoryList();


            //return Json(products,JsonRequestBehavior.AllowGet);

            ViewBag.Msz = "Product";
            ViewBag.Title = "Product";
            ViewBag.Categories = categories;
           


            return View(products);
        }
    }
}