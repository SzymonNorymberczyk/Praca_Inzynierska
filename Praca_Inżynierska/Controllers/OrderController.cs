using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        public IActionResult MyOrdersList()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var orders = _context.Orders.Include(x => x.ApplicationUser).Include(x => x.OrderDetails).Include(x => x.OrderProducts).Include(x => x.OrderStatus).Where(x => x.UserId == userId).ToList();


            return View(orders);
        }
        [HttpGet]
        [Authorize]
        public IActionResult EditOrder(int id)
        {
            var orders = _context.Orders.Include(x => x.OrderDetails).Include(x => x.OrderProducts).Include(x => x.OrderStatus).FirstOrDefault(x => x.Id == id);
            var products = _context.OrderProducts.FirstOrDefault(x => x.OrderId == id);
            var sender = _context.OrderDetails.FirstOrDefault(x => x.OrderId == id && x.TypeAdress == "Nadawca");
            var receiver = _context.OrderDetails.FirstOrDefault(x => x.OrderId == id && x.TypeAdress == "Odbiorca");



            OrderDetailViewModel orderDetailSender = new OrderDetailViewModel
            {
                Name = sender.Name,
                Surname = sender.Surname,
                City = sender.City,
                Street = sender.Street,
                Number = sender.Number,
                ZIPcode = sender.ZIPcode,
                PhoneNumber = sender.PhoneNumber,
                Email = sender.Email
            };

            OrderDetailViewModel orderDetailReceiver = new OrderDetailViewModel
            {
                Name = receiver.Name,
                Surname = receiver.Surname,
                City = receiver.City,
                Street = receiver.Street,
                Number = receiver.Number,
                ZIPcode = receiver.ZIPcode,
                PhoneNumber = receiver.PhoneNumber,
                Email = receiver.Email
            };

            OrderProductViewModel orderProductViewModel = new OrderProductViewModel
            {
                Weight = products.Weight,
                Height = products.Height,
                Length = products.Length,
                Width = products.Width,
                Count = products.Count,
                Details = products.Details

            };
            OrderViewModel orderViewModel = new OrderViewModel
            {
                Id = orders.Id,
                DateCreate = orders.DateCreate,
                Product = orderProductViewModel,
                Sender = orderDetailSender,
                Receiver = orderDetailReceiver
            };

            return View(orderViewModel);
        }

        [HttpPost]
        public IActionResult EditOrder(OrderViewModel orders)
        {

            var order = _context.Orders.FirstOrDefault(x => x.Id == orders.Id);
            var products = _context.OrderProducts.FirstOrDefault(x => x.OrderId == order.Id);
            var sender = _context.OrderDetails.FirstOrDefault(x => x.OrderId == order.Id && x.TypeAdress == "Nadawca");
            var receiver = _context.OrderDetails.FirstOrDefault(x => x.OrderId == order.Id && x.TypeAdress == "Odbiorca");
            if (order == null)
            {
                return View();
            }
            else
            {

                order.Id = orders.Id;
                order.DateCreate = orders.DateCreate;
                products.Weight = orders.Product.Weight;
                products.Height = orders.Product.Height;
                products.Length = orders.Product.Length;
                products.Width = orders.Product.Width;
                products.Count = orders.Product.Count;
                products.Details = orders.Product.Details;
                sender.Name = orders.Sender.Name;
                sender.Surname = orders.Sender.Surname;
                sender.City = orders.Sender.City;
                sender.Street = orders.Sender.Street;
                sender.Number = orders.Sender.Number;
                sender.ZIPcode = orders.Sender.ZIPcode;
                sender.PhoneNumber = orders.Sender.PhoneNumber;
                sender.Email = orders.Sender.Email;
                receiver.Name = orders.Receiver.Name;
                receiver.Surname = orders.Receiver.Surname;
                receiver.City = orders.Receiver.City;
                receiver.Street = orders.Receiver.Street;
                receiver.Number = orders.Receiver.Number;
                receiver.ZIPcode = orders.Receiver.ZIPcode;
                receiver.PhoneNumber = orders.Receiver.PhoneNumber;
                receiver.Email = orders.Receiver.Email;


                _context.SaveChanges();
                return RedirectToAction("EditOrder");
            }
        }

        [Authorize]
        public IActionResult DisplayOrder(int id)
        {
            var orders = _context.Orders.Include(x => x.OrderDetails).Include(x => x.OrderProducts).Include(x => x.OrderStatus).FirstOrDefault(x => x.Id == id);
            var products = _context.OrderProducts.FirstOrDefault(x => x.OrderId == id);
            var sender = _context.OrderDetails.FirstOrDefault(x => x.OrderId == id && x.TypeAdress == "Nadawca");
            var receiver = _context.OrderDetails.FirstOrDefault(x => x.OrderId == id && x.TypeAdress == "Odbiorca");



            OrderDetailViewModel orderDetailSender = new OrderDetailViewModel
            {
                Name = sender.Name,
                Surname = sender.Surname,
                City = sender.City,
                Street = sender.Street,
                Number = sender.Number,
                ZIPcode = sender.ZIPcode,
                PhoneNumber = sender.PhoneNumber,
                Email = sender.Email
            };

            OrderDetailViewModel orderDetailReceiver = new OrderDetailViewModel
            {
                Name = receiver.Name,
                Surname = receiver.Surname,
                City = receiver.City,
                Street = receiver.Street,
                Number = receiver.Number,
                ZIPcode = receiver.ZIPcode,
                PhoneNumber = receiver.PhoneNumber,
                Email = receiver.Email
            };

            OrderProductViewModel orderProductViewModel = new OrderProductViewModel
            {
                Weight = products.Weight,
                Height = products.Height,
                Length = products.Length,
                Width = products.Width,
                Count = products.Count,
                Details = products.Details

            };
            OrderStatusViewModel orderStatusViewModel = new OrderStatusViewModel
            {
                Name = orders.OrderStatus.Name
            };
            OrderDisplayViewModel orderViewModel = new OrderDisplayViewModel
            {
                Id = orders.Id,
                DateCreate = orders.DateCreate,
                Product = orderProductViewModel,
                Sender = orderDetailSender,
                Receiver = orderDetailReceiver,
                OrderStatus = orderStatusViewModel,
                OrderStatusId = orders.OrderStatusId


            };

            return View(orderViewModel);
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
                OrderStatusId = 1,
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
