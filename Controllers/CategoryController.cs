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

    }
}