using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HospitalWeb.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {

        public HomeController() { }

        public IActionResult Index()
        {
            return View();
        }
    }
}