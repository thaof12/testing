using Microsoft.AspNetCore.Mvc;

namespace Khachhang.Areas.Khachhang.Controllers
{

    [Area("Khachhang")]
    public class InfoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
