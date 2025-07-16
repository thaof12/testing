using Microsoft.AspNetCore.Mvc;

namespace Khachhang.Areas.Khachhang.Controllers
{

    [Area("Khachhang")]
    public class BookingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
