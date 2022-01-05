using caffetogo.Data;
using caffetogo.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Timers;

namespace caffetogo.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _db;
        private readonly IWebHostEnvironment webHost;
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext db, IWebHostEnvironment webhost)
        {
            _logger = logger;
            _db = db;
            this.webHost = webhost;
        }
        public IActionResult Index(index us)
        {
            us.product = _db.Product.ToList();
            if (us.loggedin == false)
            {
                us.Email = "Login";

            }
            return View(us);
        
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
            return View(_db.Product);
        }
        public IActionResult AdminUser()
        {
            return View(Servicies.UserConverter(_db.Users));
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateAdminUser([Bind(include: "Email,Password")] UserView values)
        {
            if (ModelState.IsValid)
            {
                UserView obj = new UserView();
                obj.Email = values.Email;
                obj.Password = values.Password;
                obj.Activity = DateTime.Now;
                _db.Users.Add(Servicies.UserCreateConverter(obj));
                _db.SaveChanges();
            }
            return RedirectToAction("AdminUser");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAdminUser([Bind(include: "Id")] UserView values)
        {
            var obj = _db.Users.Find(values.Id);
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
            if (ModelState.IsValid)
            {
                _db.Buy.Add(values);
                _db.SaveChanges();
            }
            return RedirectToAction("AdminBuy");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAdminBuy([Bind(include: "Id")] Buy values)
        {
            var obj = _db.Buy.Find(values.Id);
            _db.Buy.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("AdminBuy");
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
        public async Task<IActionResult> CreateAdminProduct([Bind(include: "item, price, Pictures")] ProductView values)
        {
            if (ModelState.IsValid)
            {
                ProductView obj = new ProductView();
                obj.item = values.item;
                obj.price = values.price;
                obj.Pictures = values.Pictures;
                string wwwR = webHost.WebRootPath;
                string filename = values.item + "-.png";
                string path = Path.Combine(wwwR, filename);
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await values.Pictures.CopyToAsync(fileStream);
                }
                Product product = Servicies.ProductCreateConverter(obj);
                _db.Product.Add(product);
                _db.SaveChanges();
            }
            return RedirectToAction("AdminProduct");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAdminProduct([Bind(include: "Id")] ProductView values)
        {
            if (values.Id < 0)
            {
                return NotFound();
            }
            var obj = _db.Product.Find(values.Id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Product.Remove(obj);
            _db.SaveChanges();
            FileInfo file = new FileInfo(Path.Combine(webHost.WebRootPath, obj.item + "-.png"));
            file.Delete();
            return RedirectToAction("AdminProduct");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateAdminProduct([Bind(include: "Id,item,price")] ProductView values)
        {
            if (ModelState.IsValid)
            {
                Product newobject = Servicies.ProductCreateConverter(values);
                var obj = _db.Product.Find(values.Id);
                _db.Product.Remove(obj);
                _db.Product.Add(newobject);
                _db.SaveChanges();
                if (obj.item != values.item)
                {
                    FileInfo file = new FileInfo(Path.Combine(webHost.WebRootPath, obj.item + "-.png"));
                    file.MoveTo(Path.Combine(webHost.WebRootPath, values.item + "-.png"));
                }
                return RedirectToAction("AdminProduct");
            }
            return View();
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser([Bind(include: "Email,Password,confirmpassword")] UserRegister values)
        {
            if (values.Password == values.confirmpassword)
            {
                UserView obj = new UserView();
                obj.Email = values.Email;
                obj.Password = values.Password;
                obj.Activity = DateTime.Now;
                _db.Users.Add(Servicies.UserCreateConverter(obj));
                _db.SaveChanges();
                return RedirectToAction("Login");
            }
            else
            {
                return RedirectToAction("Login");
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Loginok([Bind(include: "Email,Password")] UserView values)
        {
            Admin obj = new Admin();
            index ind = new index();
            ind.product = _db.Product.ToList();



            if (values.Email == obj.AdminEmail && values.Password == obj.AdminPassword)
            {
                ind.loggedin = false;
                return RedirectToAction("AdminUser");
            }
            else if (values.Email != null && values.Password != null)
            {
                User Findol = Servicies.UserCreateConverter(values);
                if (_db.Users.Any(x => x.Email == Findol.Email && x.Password == Findol.Password))
                {
                    User dba = _db.Users.Where(x => x.Email == Findol.Email && x.Password == Findol.Password).ToList()[0];
                    ind.Email = dba.Email;
                    ind.id = dba.Id;
                    ind.loggedin = true;
                    return RedirectToAction("index", ind);
                }
                else
                {
                    ind.loggedin = false;
                    return RedirectToAction("login");
                }
            }
            else
            {
                ind.loggedin = false;
                return RedirectToAction("login");

            }
        }
    }
}