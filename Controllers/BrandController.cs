using e_commerce.DataAccess;
using e_commerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace e_commerce.Controllers
{
    public class BrandController : Controller
    {
        // GET: Brand

        private readonly BrandDataAccess _brandDataAccess;

        public BrandController()
        {
            _brandDataAccess = new BrandDataAccess();
        }

        public ActionResult Index()
        {
            var brands = _brandDataAccess.BrandList();
            ViewBag.Title = "Brand";
            return View(brands);
        }


        [HttpPost]
        public ActionResult store(FormCollection formCollection, HttpPostedFileBase thumb)
        {
            var brandThumb = Utility.SaveImage(thumb,"brand");
            var name = formCollection["name"];
            var description = formCollection["description"];
            
            int status = Convert.ToInt32(formCollection["status"]);
            var result = _brandDataAccess.SaveBrand(name, description, brandThumb, status);

            return RedirectToAction("Index");
        }   


        public ActionResult Edit(int id)
        {


            var brand = _brandDataAccess.GetBrand(id);
            ViewBag.Title = "Edit Brand";
            return View(brand);
        }


        public ActionResult Delete(int id)
        {
            var result = _brandDataAccess.DeleteBrand(id);  

            return RedirectToAction("Index");
        }

        
    }
}