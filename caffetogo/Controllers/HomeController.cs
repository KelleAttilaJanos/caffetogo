using caffetogo.Data;
using caffetogo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace caffetogo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private readonly ApplicationDbContext _db;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<Buy> objList = _db.Buy;
            return View(objList);
        }
        public IActionResult Privacy()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Check()
        {
            return View();
        }
        public IActionResult AdminBuy()
        {
            return View(_db.Buy);
        }
        public IActionResult AdminProduct()
        { 
            return View(Servicies.ProductConverter(_db.Product));
        }
        public IActionResult AdminUser()
        {
            return View(Servicies.UserConverter(_db.Users));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAdminUser([Bind(include: "Email,Password")] UserView values)
        {
            UserView obj= new UserView();
            obj.Email = values.Email;
            obj.Password = values.Password;
            obj.Activity=DateTime.Now;
            _db.Users.Add(Servicies.UserCreateConverter(obj));
            _db.SaveChanges();
            return RedirectToAction("AdminUser");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAdminUser([Bind(include: "Id")] UserView values)
        {
            if (values.Id < 0)
            {
                return NotFound();
            }
            var obj = _db.Users.Find(values.Id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Users.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("AdminUser");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateAdminUser([Bind(include: "Id,Email,Password,Activity")] UserView values)
        {
            if (ModelState.IsValid)
            {
                User newobject = Servicies.UserCreateConverter(values);
                _db.Users.Update(newobject);
                _db.SaveChanges();
                return RedirectToAction("AdminUser");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAdminBuy([Bind(include: "UserId,Items")] Buy values)
        {
            _db.Buy.Add(values);
            _db.SaveChanges();
            return RedirectToAction("AdminBuy");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAdminBuy([Bind(include: "Id")] Buy values)
        {
            if (values.Id < 0)
            {
                return NotFound();
            }
            var obj = _db.Buy.Find(values.Id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Buy.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("BuyProduct");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateAdminBuy([Bind(include: "Id,UserId,Items")] Buy values)
        {
            if (ModelState.IsValid)
            {
                _db.Buy.Update(values);
                _db.SaveChanges();
                return RedirectToAction("AdminBuy");
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAdminProduct([Bind(include:"item, price, Pictures")] ProductView values)
        {
            ProductView obj = new ProductView();
            obj.item = values.item;
            obj.price = values.price;
            obj.Pictures = values.Pictures;
            _db.Product.Add(Servicies.ProductCreateConverter(obj));
            _db.SaveChanges();
            return RedirectToAction("AdminProduct");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAdminProduct([Bind(include: "Id")] ProductView values)
        {
            if(values.Id < 0 )
            {
                return NotFound();
            }
            var obj = _db.Product.Find(values.Id);
            if(obj == null)
            {
                return NotFound();
            }
            _db.Product.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("AdminProduct");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateAdminProduct([Bind(include: "Id,item,price,Pictures")] ProductView values)
        {
            if (ModelState.IsValid)
            {
            Product newobject = Servicies.ProductCreateConverter(values);
            _db.Product.Update(newobject);
            _db.SaveChanges();
            return RedirectToAction("AdminProduct");
            }
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}