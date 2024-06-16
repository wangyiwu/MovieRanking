using Microsoft.AspNetCore.Mvc;

namespace ToyProj.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
