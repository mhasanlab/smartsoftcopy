using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using work_01.Models;
using work_01.Models.ViewModel;

namespace work_01.Controllers
{
    public class HomeController : Controller
    {
        private readonly CarRentDbContext db;
        private readonly IWebHostEnvironment _hostEnv;
        public HomeController(CarRentDbContext db, IWebHostEnvironment hostEnv) 
        {
            this.db = db;
            this._hostEnv = hostEnv;
        }
        public IActionResult Index()
        {
            ViewBag.customers = db.Customers.ToList();
            return View(db.Cars.ToList());
        }

        public IActionResult Create() 
        {
            ViewBag.customers = db.Customers.ToList();
            return View();
        }

        [HttpPost]
        public IActionResult Create(CarVM vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Pictures != null)
                {
                    string newFileName = Guid.NewGuid().ToString() + "_" + vm.Pictures.FileName;
                    string newFilePath = Path.Combine("Images", newFileName);
                    string file = Path.Combine(_hostEnv.WebRootPath, newFilePath);
                    vm.Pictures.CopyTo(new FileStream(file, FileMode.Create));

                    Car car = new Car
                    {
                        CarName = vm.CarName,
                        CustomerId = vm.CustomerId,
                        Make = vm.Make,
                        PurchaseDate = vm.PurchaseDate,
                        Price = vm.Price,
                        isAvailable = vm.isAvailable,
                        PicturePath=newFileName
                    };

                    db.Cars.Add(car);
                    db.SaveChanges();
                    return PartialView("_success");
                }
            }
            else
            {
                return PartialView("_error");
            }

            ViewBag.customers = db.Customers.ToList();
            return View();
        }

        public IActionResult Edit(int? id)
        {
            Car c = db.Cars.Find(id);
            CarVM vm = new CarVM
            {
                CarId = c.CarId,
                CarName = c.CarName,
                CustomerId=c.CustomerId,
                Make = c.Make,
                PurchaseDate = c.PurchaseDate,
                Price = c.Price,
                isAvailable = c.isAvailable,
                PicturePath = c.PicturePath
            };
            ViewBag.customers = db.Customers.ToList();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Edit(CarVM vm)
        {
            if (ModelState.IsValid)
            {
                if (vm.Pictures != null)
                {
                    string newFileName = Guid.NewGuid().ToString() + "_" + vm.Pictures.FileName;
                    string newFilePath = Path.Combine("Images", newFileName);
                    string file = Path.Combine(_hostEnv.WebRootPath, newFilePath);
                    vm.Pictures.CopyTo(new FileStream(file, FileMode.Create));

                    Car c = new Car
                    {
                        CarId = vm.CarId,
                        CarName = vm.CarName,
                        CustomerId=vm.CustomerId,
                        Make = vm.Make,
                        PurchaseDate = vm.PurchaseDate,
                        Price = vm.Price,
                        isAvailable = vm.isAvailable,
                        PicturePath = newFileName
                    };
                    db.Entry(c).State = EntityState.Modified;
                    db.SaveChanges();
                    return PartialView("_updateSuccess");
                }
                else
                {
                    Car c = new Car
                    {
                        CarId = vm.CarId,
                        CarName = vm.CarName,
                        CustomerId=vm.CustomerId,
                        Make = vm.Make,
                        PurchaseDate = vm.PurchaseDate,
                        Price = vm.Price,
                        isAvailable = vm.isAvailable
                    };
                    db.Entry(c).State = EntityState.Modified;
                    db.SaveChanges();
                    return PartialView("_updateSuccess");
                }
            }
            ViewBag.customers = db.Customers.ToList();
            return View();
        }

        public IActionResult Delete(int? id) 
        {
            if (id != null) 
            {
                Car c = db.Cars.Find(id);
                db.Cars.Remove(c);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}
