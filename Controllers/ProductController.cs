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

        private readonly ProductDataAccess _dataAccess;
        public ProductController() {
            _dataAccess = new ProductDataAccess();
        
        }

        // GET: Product
        public ActionResult Index()
        {
            var products = _dataAccess.ListProduct();

            //return Json(products,JsonRequestBehavior.AllowGet);

            return View(products);
        }
    }
}