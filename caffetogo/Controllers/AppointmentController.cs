using Microsoft.AspNetCore.Mvc;

namespace caffetogo.Controllers
{
    public class AppointmentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

    }
}
