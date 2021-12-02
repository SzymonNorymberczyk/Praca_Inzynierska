using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Praca_Inżynierska.Data;
using Praca_Inżynierska.ViewModels;
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
        [HttpGet]
        public IActionResult Edit(int id)
        {
            var orders = _context.Orders.FirstOrDefault(x => x.Id == id);
            OrderViewModel orderViewModel = new OrderViewModel
            {
                Id = orders.Id,
                DateCreate = orders.DateCreate




            };
            return View(orderViewModel);

        }
        [HttpPost]
        public IActionResult Edit(OrderViewModel orderModel)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == orderModel.Id);
            if (order == null)
            {
                return View();
            }
            else
            {
                order.Id = orderModel.Id;
                order.DateCreate = orderModel.DateCreate;

                _context.SaveChanges();
                return RedirectToAction("Edit");
            }

        }
        public IActionResult OrdersList()
        {
            var test = _context.Orders.Include(x => x.ApplicationUser).Include(x => x.OrderDetails).Include(x => x.OrderProducts).OrderBy(x => x.Id).ToList();


            return View(test);
        }

        public IActionResult AdminDashBoard()
        {
            return View(@"_LayoutAdmin");
        }
    }
}
