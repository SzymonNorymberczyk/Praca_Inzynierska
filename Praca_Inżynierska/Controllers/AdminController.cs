using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Praca_Inżynierska.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Praca_Inżynierska.Controllers
{
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        public AdminController(ApplicationDbContext context)
        {

            _context = context;
        }
        public IActionResult OrdersList()
        {
            var test = _context.Orders.Include(x => x.ApplicationUser).Include(x => x.OrderDetails).Include(x => x.OrderProducts).OrderByDescending(x => x.Id).ToList();


            return View(test);
        }

        public IActionResult AdminDashBoard()
        {
            return View(@"_LayoutAdmin");
        }
    }
}
