using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Praca_Inżynierska.Data;
using Praca_Inżynierska.Models;
using Praca_Inżynierska.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace Praca_Inżynierska.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class AdminController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AdminController(ApplicationDbContext context, RoleManager<IdentityRole> roleManager, UserManager<IdentityUser> userManager)
        {
            _roleManager = roleManager;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateRole(CreateRoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                IdentityRole identityRole = new IdentityRole
                {
                    Name = model.RoleName
                };

                IdentityResult result = await _roleManager.CreateAsync(identityRole);
                if (result.Succeeded)
                {
                    return RedirectToAction("CreateRole", "Admin");
                }

                foreach (IdentityError item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }

            }
            return View(model);
        }
        [HttpGet]
        public IActionResult ListRoles()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public async Task<IActionResult> EditRole(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            EditRoleViewModel model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name
            };

            foreach (var user in _userManager.Users)
            {
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }

            return View(model);

        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync(model.Id);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {model.Id} cannot be found";
                return View("NotFound");
            }
            else
            {
                role.Name = model.RoleName;

                // Update the Role using UpdateAsync
                var result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {
                    return RedirectToAction("ListRoles");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }

                return View(model);
            }
        }

        [HttpGet]
        public async Task<IActionResult> UsersRoleList(string roleId)
        {
            ViewBag.roleId = roleId;

            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            var model = new List<UserRoleViewModel>();

            foreach (var user in _userManager.Users)
            {
                var userRoleViewModel = new UserRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };

                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }

                model.Add(userRoleViewModel);
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> UsersRoleList(List<UserRoleViewModel> model, string roleId)
        {
            var role = await _roleManager.FindByIdAsync(roleId);

            if (role == null)
            {
                ViewBag.ErrorMessage = $"Role with Id = {roleId} cannot be found";
                return View("NotFound");
            }

            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);

                IdentityResult result = null;

                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }

                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                        continue;
                    else
                        return RedirectToAction("EditRole", new { Id = roleId });
                }
            }

            return RedirectToAction("EditRole", new { Id = roleId });
        }

        [HttpGet]
        public IActionResult Edit(int id)
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
                Receiver = orderDetailReceiver,
                OrderStatusId = orders.OrderStatusId,
                StatusTypesSelectListItem = _context.OrderStatuses.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                })


            };

            return View(orderViewModel);

        }
        [HttpPost]
        public IActionResult Edit(OrderViewModel orders)
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
                order.OrderStatusId = orders.OrderStatusId;
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
                return RedirectToAction("Edit");
            }

        }
        public IActionResult OrdersList()
        {
            var orders = _context.Orders.Include(x => x.ApplicationUser).Include(x => x.OrderDetails).Include(x => x.OrderProducts).Include(x => x.OrderStatus).OrderBy(x => x.Id).ToList();


            return View(orders);
        }

        public IActionResult UsersList()
        {
            var users = _context.Users.ToList();

            return View(users);
        }

        [HttpGet]
        public IActionResult UsersEdit(string id)
        {

            var user = _context.Users.FirstOrDefault(x => x.Id == id);

            RegisterViewModel applicationUser = new RegisterViewModel
            {
                Login = user.UserName,
                Email = user.Email,
                LastName = user.LastName,
                FirstName = user.FirstName
            };

            return View(applicationUser);
        }
        [HttpPost]
        public IActionResult UsersEdit(RegisterViewModel registerViewModel)
        {

            var user = _context.Users.FirstOrDefault(x => x.Id == registerViewModel.Id);
            user.UserName = registerViewModel.Login;
            user.Email = registerViewModel.Email;
            user.LastName = registerViewModel.LastName;
            user.FirstName = registerViewModel.FirstName;

            _context.SaveChanges();
            return RedirectToAction("UsersEdit");
        }

        public IActionResult AdminDashBoard()
        {
            return View(@"_LayoutAdmin");
        }
    }
}
