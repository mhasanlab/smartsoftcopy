using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using work_01.Models;

namespace work_01.Controllers
{
    public class CategoryController : Controller
    {
        ProductsDbContext db = new ProductsDbContext();
        // GET: Category
        public ActionResult Index()
        {
            return View(db.Categories.ToList());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "categoryId,categoryName")]Category cat)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(cat);
                db.SaveChanges();
                return PartialView("_pSuccess");
            }
            return PartialView("_error"); 
        }
    }
}