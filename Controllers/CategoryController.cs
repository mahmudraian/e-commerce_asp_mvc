using e_commerce.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_commerce.Controllers
{
    public class CategoryController : Controller
    {

        private readonly CategoryDataAccess _categoryDataAccess;

        public CategoryController()
        {
            _categoryDataAccess = new CategoryDataAccess();
        }


        // GET: Category
        public ActionResult Index()
        {
            
            var categories = _categoryDataAccess.CategoryList();


            ViewBag.Msz = "Category";
            ViewBag.Title = "Category";



            return View(categories);
        }

        [HttpPost]
        public ActionResult create(FormCollection formCollection)
        {
            var name = formCollection["name"];
            var description = formCollection["description"];
            var thumb = formCollection["thumb"];
            int status = Convert.ToInt32(formCollection["status"]);
            var result = _categoryDataAccess.SaveCategory(name, description, thumb, status);

            return RedirectToAction("Index");   


        }

   

    }
}