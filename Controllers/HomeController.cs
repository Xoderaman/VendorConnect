using Microsoft.AspNetCore.Mvc;

namespace VendorConnect.Controllers
{
    public class HomeController : Controller
    {
        // Action for the homepage
        public IActionResult Index()
        {
            return View();
        }
    }
}
