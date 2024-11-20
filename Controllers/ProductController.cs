using e_commerce.DataAccess;
using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;

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
        public ActionResult Create112(FormCollection formCollection)
        {
            var name = formCollection["name"];
            var description = formCollection["description"];
            var thumb = formCollection["thumb"];
            var image = formCollection["image"];
            int status = Convert.ToInt32(formCollection["status"]);
            int stock = Convert.ToInt32(formCollection["stock"]);
            int quantity = Convert.ToInt32(formCollection["quantity"]);
            int category_id = Convert.ToInt32(formCollection["category_id"]);
            int brand_id = Convert.ToInt32(formCollection["brand_id"]);
            Decimal price = Convert.ToDecimal(formCollection["price"]);

            var title = formCollection["title"];




            var result = _productdataAccess.saveProduct(name, description, thumb,image, status, stock, quantity, title,price, category_id, brand_id);

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
               


               

               

                // Return the customized JSON response
                return Json(new
                {
                    success = true,
                    message = "Product list retrieved successfully.",
                    data = products
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


        public ActionResult Edit(int id)
        {
            var product = _productdataAccess.SingleProduct(id);
          
            var categories = _categorydataaccess.CategoryList();
            ViewBag.Categories = categories;

            return View(product);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection formCollection)
        {
            try
            {
                var id = Convert.ToInt32(formCollection["Id"]);
                var name = formCollection["Name"];
                var description = formCollection["Description"];
                var thumb = formCollection["ThumbnailUrl"];
                var image = formCollection["ImageUrl"];
                int status = Convert.ToInt32(formCollection["Status"]);
                int stock = Convert.ToInt32(formCollection["Stock"]);
                int quantity = Convert.ToInt32(formCollection["Quantity"]);
                var title = formCollection["Title"];
                Decimal price = Convert.ToDecimal(formCollection["Price"]);
                int category_id = Convert.ToInt32(formCollection["CategoryId"]);
                int brand_id = Convert.ToInt32(formCollection["BrandId"]);


                var result = _productdataAccess.UpdateProduct(id, name, description, thumb, image, status, stock, quantity, title, price, category_id, brand_id);
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return Json(new
                {
                    success = false,
                    message = "An error occurred while updating the product.",
                    error = ex.Message
                }, JsonRequestBehavior.AllowGet);
            }



        }


        public ActionResult Create(FormCollection formCollection , HttpPostedFileBase ImageUrl)
        {
           
            
            var imageurl = SaveImage(ImageUrl);
            int result = 0;
            if (ModelState.IsValid)
            {
               
                var name = formCollection["Name"];
                var description = formCollection["Description"];
                var thumb = formCollection["ThumbnailUrl"];
                int status = Convert.ToInt32(formCollection["Status"]);
                int stock = Convert.ToInt32(formCollection["Stock"]);
                int quantity = Convert.ToInt32(formCollection["Quantity"]);
                var title = formCollection["Title"];
                decimal price = Convert.ToDecimal(formCollection["Price"]);
                int  category_id = Convert.ToInt32(formCollection["CategoryId"]);
                int  brand_id = Convert.ToInt32(formCollection["BrandId"]);





                result = _productdataAccess.saveProduct(name, description,imageurl,thumb, status, stock, quantity, title, price, category_id, brand_id);
            }
            return RedirectToAction("Index");
        }


        private string SaveImage(HttpPostedFileBase image)
        {
            if (image != null && image.ContentLength > 0)
            {
                // Validate file type (e.g., only allow JPEG, PNG)
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
                var fileExtension = Path.GetExtension(image.FileName).ToLower();

                if (!allowedExtensions.Contains(fileExtension))
                {
                    throw new InvalidOperationException("Invalid file format. Please upload a valid image.");
                }

                // Generate unique file name to avoid overwrites
                var uniqueFileName = Guid.NewGuid() + fileExtension;

                // Define the path to save the image
                var imageFolder = "~/Images/";
                var physicalPath = Path.Combine(HttpContext.Server.MapPath(imageFolder), uniqueFileName);

                // Ensure the directory exists
                var directory = Path.GetDirectoryName(physicalPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Save the image
                image.SaveAs(physicalPath);

                // Return the relative URL to save in the database
                return imageFolder + uniqueFileName;
            }

            return null; // Return null if no image is uploaded
        }


        public ActionResult Delete(int id)
        {
            var result = _productdataAccess.DeleteProduct(id);
            return RedirectToAction("Index");

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