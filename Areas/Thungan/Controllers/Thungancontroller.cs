using Microsoft.AspNetCore.Mvc;

namespace SP6.Areas.Thungan.Controllers
{
    [Area("Thungan")]
    public class ThunganController : Controller
    {
        public IActionResult Index() => View();
        public IActionResult DSdatcho() => View();
        public IActionResult Pay() => View();
    }
}
