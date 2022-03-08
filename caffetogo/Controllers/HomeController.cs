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
        [HttpPost]
        public IActionResult Login([Bind(include: "bucket")] Logins values)
        {
            return View(values);
        }
        public IActionResult Confirm(UserRegister obj)
        {
            return View(obj);
        }
        public IActionResult Forgottenpassword(Logins log)
        {
            return View(log);
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
                string MailBody = "";
                string email = "";
                using (StreamReader reader = new StreamReader(webHost.WebRootPath + @"\purchase.html"))
                {
                    email = reader.ReadToEnd();
                }
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
                MailBody += "<hr>";
                MailBody += "<p>Összesen " + summa + " Ft</p>";
                email = email.Replace("{bod}", MailBody);
                string to = _db.Users.Where(x => x.Id == values.UserId).Select(x => x.Email).SingleOrDefault();
                string from = "caffetogotest@gmail.com";
                MailMessage message = new MailMessage(from, to);
                message.Subject = "Sikeres Rendelés";
                message.Body = email;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
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
        [HttpPost]
        public IActionResult Forgottenpassword1([Bind(include: "email,bucket")] Logins values)
        {
            if (_db.Users.Any(x => x.Email == values.email))
            {
                string MailBody = "";
                string email = "";
                using (StreamReader reader = new StreamReader(webHost.WebRootPath + @"\pwchanges.html"))
                {
                    email = reader.ReadToEnd();
                }
                string to = values.email;
                string from = "caffetogotest@gmail.com";
                MailMessage message = new MailMessage(from, to);
                MailBody = "<a href = 'https://localhost:44354/Home/Forgottenpassword?email=" + values.email + "' > Új jelszó kérése </a>";
                email = email.Replace("{link}", MailBody);
                message.Subject = "Elfelejtett jelszó";
                message.Body = email;
                message.BodyEncoding = Encoding.UTF8;
                message.IsBodyHtml = true;
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                System.Net.NetworkCredential basicCredential1 = new
                System.Net.NetworkCredential("caffetogotest@gmail.com", "210d7730");
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                client.Credentials = basicCredential1;
                try
                {
                    client.Send(message);
                    index inde = new index();
                    inde.message = HttpUtility.UrlEncode("Változtasd meg a jelszavad majd próbálkozz újra");
                    inde.cart = values.bucket;
                    return RedirectToAction("index", inde);
                }
                catch
                {
                    index inde = new index();
                    inde.message = HttpUtility.UrlEncode("A jelszó változtató email küldése sikertelen");
                    inde.cart = values.bucket;
                    return RedirectToAction("index", inde);
                }
            }
            else
            {
                index inde = new index();
                inde.message = HttpUtility.UrlEncode("Az email cím nem létezik");
                inde.cart = values.bucket;
                return RedirectToAction("index", inde);
            }
        }
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser([Bind(include: "Email,Password,confirmpassword,cart")] UserRegister values)
        {
            User us = new User();
            UserView obj = new UserView();
            index ind = new index();
            if (_db.Users.Any(x => x.Email == values.Email))
            {
                ind.message = HttpUtility.UrlEncode("Ez az emailcím már foglalt");
                ind.cart = values.cart;
                return RedirectToAction("Index", ind);
            }
            else
            {
                if (values.Password == values.confirmpassword)
                {
                    string MailBody = "";
                    string email = "";
                    using (StreamReader reader = new StreamReader(webHost.WebRootPath + @"\confirm.html"))
                    {
                        email = reader.ReadToEnd();
                    }
                    string to = values.Email;
                    string from = "caffetogotest@gmail.com";
                    MailMessage message = new MailMessage(from, to);
                    MailBody = "<a href = 'https://localhost:44354/Home/Confirm?Email=" + values.Email + "&Password=" + values.Password + "' > megerősítő link </a>";
                    email = email.Replace("{link}", MailBody);
                    message.Subject = "Regisztráció hitelesítése";
                    message.Body = email;
                    message.BodyEncoding = Encoding.UTF8;
                    message.IsBodyHtml = true;
                    SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                    System.Net.NetworkCredential basicCredential1 = new
                    System.Net.NetworkCredential("caffetogotest@gmail.com", "210d7730");
                    client.EnableSsl = true;
                    client.UseDefaultCredentials = false;
                    client.Credentials = basicCredential1;
                    try
                    {
                        client.Send(message);
                    }
                    catch
                    {
                        index inde = new index();
                        inde.message = HttpUtility.UrlEncode("A felhasználó létrehozása sikertelen");
                        inde.cart = values.cart;
                        return RedirectToAction("index", inde);
                    }
                    index inda = new index();
                    inda.message = HttpUtility.UrlEncode("Kérjük hitelesítse regisztrációját");
                    inda.cart = values.cart;
                    return RedirectToAction("index", inda);
                }
                else
                {
                    index indb = new index();
                    indb.message = HttpUtility.UrlEncode("A jelszavak nem egyeznek");
                    indb.cart = values.cart;
                    return RedirectToAction("index", indb);
                }
            }
        }
        [HttpPost]
        public IActionResult UpdateUser([Bind(include: "Email,Password,confirmpassword")] UserRegister values)
        {
            index ind = new index();
            if (values.Password == values.confirmpassword)
            {
                UserView obj = new UserView();
                obj.Email = values.Email;
                obj.Password = values.confirmpassword;
                User user = Servicies.UserCreateConverter(obj);
                User us = _db.Users.Where(x => x.Email == obj.Email).FirstOrDefault();
                us.Password = user.Password;
                try
                {
                    _db.SaveChanges();
                    ind.message = HttpUtility.UrlEncode("Sikeres jelszóváltoztatás");
                }
                catch
                {
                    ind.message = HttpUtility.UrlEncode("hiba a mentés során");
                }
            }
            else
            {
                ind.message = HttpUtility.UrlEncode("A jelszavak nem eggyeznek");
            }
            return RedirectToAction("index", ind);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateUser2([Bind(include: "Email,Password")] UserRegister values)
        {
            UserView obj = new UserView();
            index inda = new index();
            try
            {
                if (values.Email == null || values.Password == null)
                {
                    inda.message = HttpUtility.UrlEncode("Sikertelen hitelesítés");
                }
                else
                {
                    obj.Email = values.Email;
                    obj.Password = values.Password;
                    obj.Activity = DateTime.Now;
                    _db.Users.Add(Servicies.UserCreateConverter(obj));
                    _db.SaveChanges();
                    inda.message = HttpUtility.UrlEncode("Sikeres hitelesítés");
                }
            }
            catch
            {
                inda.message = HttpUtility.UrlEncode("Sikertelen hitelesítés");
            }
            return RedirectToAction("index", inda);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Loginok([Bind(include: "Email,Password,cart")] UserView values)
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
                    ind.cart = values.cart;
                    return RedirectToAction("index", ind);
                }
                else
                {
                    index inder = new index();
                    inder.message = HttpUtility.UrlEncode("Az Email vagy a jelszó nem megfelelő!");
                    inder.cart = values.cart;
                    return RedirectToAction("index", inder);
                }
            }
            else
            {
                index inde = new index();
                inde.message = HttpUtility.UrlEncode("Az Email vagy a jelszó nem megfelelő!");
                inde.cart = values.cart;
                return RedirectToAction("index", inde);
            }
        }
    }
}