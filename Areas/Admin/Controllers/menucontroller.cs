using System;
using System.Linq;
//using LittleFishStation.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LittleFishStation.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MenuController : Controller
    {
        public IActionResult Menu()
        {
            return View();
        }

        public IActionResult Combo()
        {
            return View();
        }

    }
}
