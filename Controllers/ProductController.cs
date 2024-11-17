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
        [Route("admin/product")]
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

        [HttpPost]
        public ActionResult Create(FormCollection formCollection)
        {
            var name = formCollection["name"];
            var description = formCollection["description"];
            var thumb = formCollection["thumb"];
            int status = Convert.ToInt32(formCollection["status"]);
            int stock = Convert.ToInt32(formCollection["stock"]);
            int quantity = Convert.ToInt32(formCollection["quantity"]);
            int category_id = Convert.ToInt32(formCollection["category_id"]);
            int brand_id = Convert.ToInt32(formCollection["brand_id"]);
            Decimal price = Convert.ToDecimal(formCollection["price"]);

            var title = formCollection["title"];




            var result = _productdataAccess.saveProduct(name, description, thumb, status, stock, quantity, title,price, category_id, brand_id);

            return RedirectToAction("Index");


        }

        [Route("api/product")]

        //public ActionResult ListProduct()
        //{
        //    var products = _productdataAccess.ListProduct();

        //    return Json(products, JsonRequestBehavior.AllowGet);
        //}


        public ActionResult ListProduct()
        {
            try
            {
                // Retrieve the list of products
                var products = _productdataAccess.ListProduct();
               // return Json(products, JsonRequestBehavior.AllowGet);
                // Optional: Customize or filter the product data
                  var response = products.Select(p => new
                {
                    ProductId = p.ProductId,
                    Name = p.ProductName.Trim(), // Trimming extra spaces
                    Description = p.ProductDescription,
                    Price = p.ProductPrice,
                    Stock = p.ProductCount,
                    Categpries= (from cat in p.Categories.AsEnumerable()
                                 select new
                                 {
                                     categoryName = cat.Name,
                                     categoryDescription = cat.Description



                                 }),


                      Categories = (p.Categories != null && p.Categories.Any())
                    ? p.Categories
                    : null // Include Categories only if not null or empty


                  }
                

                );

                // Return the customized JSON response
                return Json(new
                {
                    success = true,
                    message = "Product list retrieved successfully.",
                    data = response
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                // Handle errors gracefully
                return Json(new
                {
                    success = false,
                    message = "An error occurred while retrieving the product list.",
                    error = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }




    }
}


//([name]
//         , [description]
//         , [category_id]
//         , [brand_id]
//         , [stock]
//         , [quantity]
//         , [title]
//         , [status]
//         , [created_at]
//         , [updated_at]
//         , [thumb])