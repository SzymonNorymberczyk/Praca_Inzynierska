using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Praca_Inżynierska.Controllers
{
    public class AdminController : Controller
    {
        public IActionResult OrdersList()
        {
            return View();
        }

        public IActionResult AdminDashBoard()
        {
            return View(@"_LayoutAdmin");
        }
    }
}
