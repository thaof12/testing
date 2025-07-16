
using System;
using System.Linq;
//using LittleFishStation.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LittleFishStation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class NhanvienController : Controller
    {
        public IActionResult Nhanvien()
        {
            return View();
        }

    }
}
