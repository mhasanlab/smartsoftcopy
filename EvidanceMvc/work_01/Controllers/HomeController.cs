using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using work_01.Models;

namespace work_01.Controllers
{
    public class HomeController : Controller
    {
        ProductsDbContext db =new ProductsDbContext();
        // GET: Home
        public ActionResult Index()
        {
            return View(db.Products.ToList());
        }

        //Add New Products
        public ActionResult Create()
        {
            ViewBag.categoryList = new SelectList(db.Categories, "categoryId", "categoryName");
            return View();
        }
        [HttpPost]
        
        public ActionResult Create(ProductViewModel pro)
        {
            if (ModelState.IsValid)
            {
                if (pro.Picture!=null)
                {
                    string filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(pro.Picture.FileName));
                    pro.Picture.SaveAs(Server.MapPath(filePath));

                    Product p = new Product
                    {
                        productId = pro.productId,
                        productName = pro.productName,
                        catId = pro.catId,
                        price = pro.price,
                        storeDate = pro.storeDate,
                        picturePath = filePath
                    };
                    db.Products.Add(p);
                    db.SaveChanges();
                    return PartialView("_pSuccess");
                }
               
            }
            ViewBag.categoryList = new SelectList(db.Categories, "categoryId", "categoryName");
            return PartialView("_error");
        }


        // EDIT PRODUCTS
        public ActionResult Edit(int id)
        {
            //if (id == null)
            //{
            //    return new HttpStatusCodeResult(System.Net.HttpStatusCode.BadRequest);
            //}
            Product pro = db.Products.Find(id);
            //if (pro == null)
            //{
            //    return HttpNotFound();
            //}
            ProductViewModel p = new ProductViewModel
            {
                productId = pro.productId,
                productName = pro.productName,
                catId = (int)pro.catId,
                price = pro.price,
                storeDate = pro.storeDate,
                picturePath = pro.picturePath
            };
            ViewBag.categoryList = new SelectList(db.Categories, "categoryId", "categoryName");
            return View(p);
        }
        [HttpPost]
        public ActionResult Edit(ProductViewModel pview)
        {
            if (ModelState.IsValid)
            {
                string filePath = pview.picturePath;
                if (pview.Picture != null)
                {
                    filePath = Path.Combine("~/Images", Guid.NewGuid().ToString() + Path.GetExtension(pview.Picture.FileName));
                    pview.Picture.SaveAs(Server.MapPath(filePath));

                    Product p = new Product
                    {
                        productId = pview.productId,
                        productName = pview.productName,
                        catId = pview.catId,
                        price = pview.price,
                        storeDate = pview.storeDate,
                        picturePath = filePath
                    };
                    db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    Product p = new Product
                    {
                        productId = pview.productId,
                        productName = pview.productName,
                        catId = pview.catId,
                        price = pview.price,
                        storeDate = pview.storeDate,
                        picturePath = filePath
                    };
                    db.Entry(p).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            ViewBag.categoryList = new SelectList(db.Categories, "categoryId", "categoryName");
            return View(pview);
        }


        // Delete Product
        public ActionResult Delete(int? id)
        {
            Product product = db.Products.Find(id);
            return View(product);
        }
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult ConfirmDelete(int id)
        {
            Product pro = db.Products.Find(id);
            string file_name = pro.picturePath;
            string path = Server.MapPath(file_name);
            FileInfo file = new FileInfo(path);
            if (file.Exists)
            {
                file.Delete();
            }
            db.Products.Remove(pro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}