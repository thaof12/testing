
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LittleFishStation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class KaraController : Controller
    {
        public IActionResult danhsachphong()
        {
            return View();
        }

        public IActionResult Gia()
        {
            return View();
        }

        public IActionResult LichSu()
        {
            return View();
        }

        public IActionResult TrangThai()
        {
            return View();
        }
    }
}
