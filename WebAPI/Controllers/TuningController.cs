using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    public class TuningController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
