using Microsoft.AspNetCore.Mvc;

namespace fit_box.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
