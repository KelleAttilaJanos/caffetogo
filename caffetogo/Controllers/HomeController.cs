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
            IEnumerable<Buy> buy = _db.Buy;
            return View(buy);
        }
        public IActionResult CreateAdminBuy()
        {
            return View();
        }
        public IActionResult AdminProduct()
        {
            IEnumerable<Product> products = _db.Product;
            return View(products);
        }
        public IActionResult CreateAdminProduct()
        {
            return View();
        }
        public IActionResult AdminUser()
        {
            IEnumerable<User> user = _db.Users;
            return View(user);
        }
        public IActionResult CreateAdminUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAdminUser(User obj)
        {
            _db.Users.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("CreateAdminUser");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAdminBuy(Buy obj)
        {
            _db.Buy.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("CreateAdminBuy");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAdminProduct(Product obj)
        {
            _db.Product.Add(obj);
            _db.SaveChanges();
            return RedirectToAction("CreateAdminProduct");
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
