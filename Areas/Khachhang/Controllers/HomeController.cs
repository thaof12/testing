using Microsoft.AspNetCore.Mvc;

namespace Khachhang.Areas.Khachhang.Controllers
{
    [Area("Khachhang")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
