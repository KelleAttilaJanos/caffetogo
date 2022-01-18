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
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
                us.Email = HttpUtility.UrlEncode("Belépés");
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
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Check([Bind(include: "Cart,Id,Email")] Check ch)
        {
            ch.Products = _db.Product.ToList();
            return View(ch);
        }
        public IActionResult Admin(Admin admin)
        {
            if (admin.Email == admin.AdminEmail && admin.Password == admin.AdminPassword)
            {
                AdminView adminView = new AdminView();
                adminView.buys = _db.Buy;
                adminView.products = _db.Product;
                adminView.users = Servicies.UserConverter(_db.Users);
                return View(adminView);
            }
            else
            {
                return RedirectToAction("Index");
            }
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
            Admin admin = new Admin();
            admin.Email = admin.AdminEmail;
            admin.Password = admin.AdminPassword;
            return RedirectToAction("Admin", admin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAdminUser([Bind(include: "Id")] UserView values)
        {
            var obj = _db.Users.Find(values.Id);
            _db.Users.Remove(obj);
            _db.SaveChanges();
            Admin admin = new Admin();
            admin.Email = admin.AdminEmail;
            admin.Password = admin.AdminPassword;
            return RedirectToAction("Admin", admin);
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
                Admin admin = new Admin();
                admin.Email = admin.AdminEmail;
                admin.Password = admin.AdminPassword;
                return RedirectToAction("Admin", admin);
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
            Admin admin = new Admin();
            admin.Email = admin.AdminEmail;
            admin.Password = admin.AdminPassword;
            return RedirectToAction("Admin", admin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateBuy([Bind(include: "UserId,Items")] Buy values)
        {
            index ind = new index();
            if (ModelState.IsValid)
            {
                IEnumerable<Product> products = _db.Product;
                List<string> itemlist = new List<string>();
                List<int> itemcount = new List<int>();
                List<string> list = values.Items.Split(',').ToList();
                for (int i = 0; i < list.Count(); i++)
                {
                    bool check = false;
                    if (itemlist.Count() > 0)
                    {
                        for (int j = 0; j < itemlist.Count(); j++)
                        {
                            if (list[i] == itemlist[j])
                            {
                                itemcount[j]++;
                                check = true;
                            }
                        }
                    }
                    if (check == false)
                    {
                        itemlist.Add(list[i]);
                        itemcount.Add(1);
                    }

                }
                var MailBody = "";
                int summa = 0;
                for (int i = 0; i < itemlist.Count(); i++)
                {
                    foreach (Product item in products)
                    {
                        if (item.Id == Convert.ToInt32(itemlist[i]))
                        {
                            summa += itemcount[i] * item.price;
                            MailBody += "<p>" + itemcount[i] + "db " + item.item + " " + itemcount[i] * item.price + " Ft</p>";
                        }
                    }
                }
                MailBody += "<p>Összesen " + summa + " ft</p>";
                // read the content of template and pass it to the Email constructor


                var usmal = _db.Users.Where(x => x.Id == values.UserId).Select(x => x.Email).SingleOrDefault();
                string to = usmal; //To address    
                string from = "caffetogotest@gmail.com"; //From address    
                MailMessage message = new MailMessage(from, to);
                string mailbody = MailBody;
                message.Subject = "Sikeres Rendelés";
                message.Body = mailbody;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
                System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential("caffetogotest@gmail.com", "210d7730");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicCredential1;


                try
                {
                    client.Send(message);
                    _db.Buy.Add(values);
                    _db.SaveChanges();
                    ind.message = HttpUtility.UrlEncode("Köszönjük a vásárlást");
                }

                catch
                {
                    ind.message = HttpUtility.UrlEncode("Hiba a kérés feldolgozásakor");
                }


            }
            return RedirectToAction("Index", ind);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteAdminBuy([Bind(include: "Id")] Buy values)
        {
            var obj = _db.Buy.Find(values.Id);
            _db.Buy.Remove(obj);
            _db.SaveChanges();
            Admin admin = new Admin();
            admin.Email = admin.AdminEmail;
            admin.Password = admin.AdminPassword;
            return RedirectToAction("Admin", admin);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateAdminBuy([Bind(include: "Id,UserId,Items")] Buy values)
        {
            if (ModelState.IsValid)
            {
                _db.Buy.Update(values);
                _db.SaveChanges();
                Admin admin = new Admin();
                admin.Email = admin.AdminEmail;
                admin.Password = admin.AdminPassword;
                return RedirectToAction("Admin", admin);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateAdminProduct([Bind(include: "item,price,Pictures")] ProductView values)
        {
            if (ModelState.IsValid)
            {
                ProductView obj = new ProductView();
                obj.item = values.item;
                obj.price = values.price;
                obj.Pictures = values.Pictures;
                if (values.Pictures != null)
                {
                    string wwwR = webHost.WebRootPath;
                    string filename = values.item + "-.png";
                    string path = Path.Combine(wwwR, filename);
                    using (FileStream fileStream = new FileStream(path, FileMode.Create))
                    {
                        await values.Pictures.CopyToAsync(fileStream);
                    }
                }                
                _db.Product.Add(Servicies.ProductCreateConverter(obj));
                _db.SaveChanges();
            }
            Admin admin = new Admin();
            admin.Email = admin.AdminEmail;
            admin.Password = admin.AdminPassword;
            return RedirectToAction("Admin", admin);
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
            try
            {
                FileInfo file = new FileInfo(Path.Combine(webHost.WebRootPath, obj.item + "-.png"));
                file.Delete();
            }
            catch
            {
                index ind = new index();
                ind.message = "A kép törlése sikertelen volt";
                return RedirectToAction("Index", ind);
            }
            Admin admin = new Admin();
            admin.Email = admin.AdminEmail;
            admin.Password = admin.AdminPassword;
            return RedirectToAction("Admin", admin);
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
                try
                {
                    if (obj.item != values.item)
                    {
                        FileInfo file = new FileInfo(Path.Combine(webHost.WebRootPath, obj.item + "-.png"));
                        file.MoveTo(Path.Combine(webHost.WebRootPath, values.item + "-.png"));
                    }
                }
                catch
                {
                    index ind = new index();
                    ind.message = "A kép átnevezése sikertelen volt";
                    return RedirectToAction("Index", ind);
                }
                Admin admin = new Admin();
                admin.Email = admin.AdminEmail;
                admin.Password = admin.AdminPassword;
                return RedirectToAction("Admin", admin);
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
            User us = new User();
            UserView obj = new UserView();
            index ind = new index();
            if (_db.Users.Any(x => x.Email == values.Email))
            {
                ind.message = HttpUtility.UrlEncode("Ez az emailcím már foglalt");
                return RedirectToAction("Index", ind);
            }
            else
            {
                if (values.Password == values.confirmpassword)
                {
                    string to = values.Email; //To address    
                    string from = "caffetogotest@gmail.com"; //From address    
                    MailMessage message = new MailMessage(from, to);
                    string mailbody = "Gratulálok sikeresen regisztráltál a CaffeToGo alkalmazásunkba" + "Az Ön Email címe:" + values.Email + "Az Ön jelszava:" + values.Password;
                    message.Subject = "Sikeres Regisztáció";
                    message.Body = mailbody;
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587); //Gmail smtp    
                    System.Net.NetworkCredential basicCredential1 = new
                    System.Net.NetworkCredential("caffetogotest@gmail.com", "210d7730");
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = basicCredential1;
                    try
                    {
                        client.Send(message);
                        obj.Email = values.Email;
                        obj.Password = values.Password;
                        obj.Activity = DateTime.Now;
                        _db.Users.Add(Servicies.UserCreateConverter(obj));
                        _db.SaveChanges();
                    }
                    catch
                    {
                        index inde = new index();
                        inde.message = HttpUtility.UrlEncode("A felhasználó létrehozása sikertelen");
                        return RedirectToAction("index", inde);
                    }
                    index inda = new index();
                    inda.message = HttpUtility.UrlEncode("Sikeres regisztráció,Emailben küldjük az adatokat");
                    return RedirectToAction("index", inda);
                }
                else
                {
                    index indb = new index();
                    indb.message = HttpUtility.UrlEncode("A jelszavak nem egyeznek");
                    return RedirectToAction("index", indb);
                }
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
                obj.Email = values.Email;
                obj.Password = values.Password;
                return RedirectToAction("Admin", obj);
            }
            else if (values.Email != null && values.Password != null)
            {
                User Findol = Servicies.UserCreateConverter(values);
                if (_db.Users.Any(x => x.Email == Findol.Email && x.Password == Findol.Password))
                {
                    User dba = _db.Users.Where(x => x.Email == Findol.Email && x.Password == Findol.Password).ToList()[0];
                    ind.Email = HttpUtility.UrlEncode(dba.Email);
                    ind.id = HttpUtility.UrlEncode(dba.Id.ToString());
                    ind.loggedin = true;
                    ind.message = HttpUtility.UrlEncode("Sikeres bejelentkezés");
                    return RedirectToAction("index", ind);
                }
                else
                {
                    index inder = new index();
                    inder.message = HttpUtility.UrlEncode("Az Email vagy a jelszó nem megfelelő!");
                    return RedirectToAction("index", inder);
                }
            }
            else
            {
                index inde = new index();
                inde.message = HttpUtility.UrlEncode("Az Email vagy a jelszó nem megfelelő!");
                return RedirectToAction("index", inde);
            }
        }
    }
}