using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Praca_Inżynierska.Data;
using Praca_Inżynierska.Models;
using Praca_Inżynierska.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Praca_Inżynierska.Controllers
{
    public class OrderController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public OrderController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public IActionResult AddOrder()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public IActionResult AddOrder(OrderViewModel orderModel)
        {

            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var product = new OrderProduct
            {
                Weight = orderModel.Product.Weight,
                Height = orderModel.Product.Height,
                Length = orderModel.Product.Length,
                Width = orderModel.Product.Width,
                Count = orderModel.Product.Count,
                Details = orderModel.Product.Details
            };
            var sender = new OrderDetail
            {
                
                Name = orderModel.Sender.Name,
                Surname = orderModel.Sender.Surname,
                PhoneNumber = orderModel.Sender.PhoneNumber,
                City = orderModel.Sender.City,
                Street = orderModel.Sender.Street,
                Number = orderModel.Sender.Number,
                ZIPcode = orderModel.Sender.ZIPcode,
                TypeAdress = "Nadawca"


            };
            var receiver = new OrderDetail
            {
                Name = orderModel.Receiver.Name,
                Surname = orderModel.Receiver.Surname,
                PhoneNumber = orderModel.Receiver.PhoneNumber,
                City = orderModel.Receiver.City,
                Street = orderModel.Receiver.Street,
                Number = orderModel.Receiver.Number,
                ZIPcode = orderModel.Receiver.ZIPcode,
                TypeAdress = "Odbiorca"
            };
            var order = new Order
            {
                DateCreate = DateTime.Now,
                UserId = userId,
                OrderDetails = new List<OrderDetail>
                {
                    sender,
                    receiver
                },
                OrderProducts = new List<OrderProduct>
                {
                    product
                }

                
            };
            _context.Orders.Add(order);
            _context.SaveChanges();
            //int id = _context.Orders.OrderBy(x => x.Id).LastOrDefault().Id;
            //return RedirectToAction(nameof(AdvertisementDisplay), new { id = id, adName = adModel.Title });
            return View();
        }
    }
}
