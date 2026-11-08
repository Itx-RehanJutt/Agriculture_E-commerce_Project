using DbEntities.Entities;
using Infrastructure.Implementation;
using Infrastructure.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace My.Project.Controllers
{
    public class OrderController : Controller
    {
        private IOrderRepository _orderRepository;
        private IAddressRepository _addressRepository;

        public OrderController(IOrderRepository orderRepository , IAddressRepository addressRepository)
        {
            _orderRepository = orderRepository;
            _addressRepository = addressRepository;
        }
        [Authorize]
        public async Task<IActionResult> Order()
        {
            var customerId = Convert.ToInt32(HttpContext.Session.GetInt32("CustomerId"));
            var existingAddress = await _addressRepository.GetAddressById(customerId);
             ViewBag.CustomerId = customerId;
            ViewBag.HasAddress = existingAddress != null;
           
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderCartVM model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fill out the form Correctly.");
                return View("Order", model);
            }
            await _orderRepository.AddOrder(model);
            return RedirectToAction("Shop");
        }
        [HttpPost]
        public async Task<IActionResult> CreateAddress(AddressCreateVM model)
        {
            var customerId = Convert.ToInt32(HttpContext.Session.GetInt32("CustomerId"));
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Please fill out the form Correctly.");
                if (TempData["OrderModel"] == null)
                {
                    return View("OrderCart", null);
                }
                string modelJson = TempData["OrderModel"] as string;
                var orderModel = Newtonsoft.Json.JsonConvert.DeserializeObject<OrderCartVM>(modelJson);

                var viewModel = new OrderPageVM
                {
                    Address = model,
                    OrderCart = orderModel
                };
                return View("Order", viewModel);
            }
            await _addressRepository.AddAddress(model);
            return ViewComponent("ShippingAddress", new { customerId});
        }
    }
}
