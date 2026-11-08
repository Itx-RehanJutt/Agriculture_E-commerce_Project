using DbEntities.Entities;
using Infrastructure.Implementation;
using Infrastructure.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace My.Project.Controllers
{
    [Authorize]
    public class CartController : Controller
    {
        private readonly ICartRepository _cartRepository;
        private readonly IOrderRepository _orderRepository;

        public CartController(ICartRepository cartRepository , IOrderRepository orderRepository)
        {
            _cartRepository = cartRepository;
            _orderRepository = orderRepository;
        }

        [HttpGet]
        public async Task<IActionResult> CartItem()
        {
            var loginId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            var cart = await _cartRepository.GetCartItemsByLoginId(loginId);
            return View(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart(int productId, int quantity , string redirectTo)
        {
            var loginId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            var customerId = Convert.ToInt32(HttpContext.Session.GetInt32("CustomerId"));
            await _cartRepository.AddToCart(loginId , productId , quantity);
            if (redirectTo == "Order")
            {
                // In your controller or wherever you set TempData:
                var orderModel = await _orderRepository.OrderCart(productId, quantity, customerId);
                HttpContext.Session.SetString("OrderModel", JsonConvert.SerializeObject(orderModel));
                return RedirectToAction("Order", "Order", new { productId = productId });
            }

            return RedirectToAction("Shop", "Home");
        }

        //[HttpPost]
        //public async Task<IActionResult> UpdateQuantity([FromBody] CartItemUpdateVM request)
        //{
        //    if (request.Quantity < 1)
        //    {
        //        return BadRequest("Quantity must be at least 1.");
        //    }
        //    // Call repository to update only the specific item
        //    var cartItem = await _cartRepository.UpdateCart(request);
        //    if (cartItem == null)
        //    {
        //        return BadRequest(new { message = "Cart item not found or update failed." });
        //    }

        //    return Json(new
        //    {
        //        quantity = cartItem.Quantity,
        //        totalPrice = cartItem.TotalPrice
        //    });
        //}
        [HttpPost]
        public async Task<IActionResult> UpdateMultipleCartItems([FromBody] CartUpdateVM request)
        {
            if (request.CartItems == null || request.CartItems.Count == 0)
            {
                return BadRequest("No items selected.");
            }

            decimal totalCartPrice = 0;
            int totalquantity = 0;
            List<CartItemUpdateVM> updatedItems = new List<CartItemUpdateVM>();

            foreach (var item in request.CartItems)
            {
                var updatedItem = await _cartRepository.UpdateCart(item);
                if (updatedItem != null)
                {
                    updatedItems.Add(updatedItem);
                    totalCartPrice += updatedItem.TotalPrice;
                    totalquantity += updatedItem.Quantity;
                }
            }

            return Json(new
            {
                items = updatedItems,
                totalPrice = totalCartPrice,
                totalQuantity = totalquantity
            });
        }


        [HttpPost]
        public async Task<JsonResult> GetCartData([FromBody] List<int> productIds)
        {
            var cart = await _cartRepository.GetCartItems(productIds);

            return Json(new
            {
                success = true,
                data = new
                {
                    cart.Quantity,
                    cart.TotalPrice
                }
            });
        }
        [HttpGet]
        public async Task<IActionResult> GetCartItemCount()
        {
            var loginId = Convert.ToInt32(HttpContext.Session.GetInt32("UserId"));
            int cartCount = await _cartRepository.GetCartCount(loginId); // Ye function database se count fetch karega
            return Json(cartCount);
        }

        [HttpPost]
        public async Task<IActionResult> DeleteCartItem([FromBody] int productId)
        {
            var cartItem = await _cartRepository.DeleteCartItem(productId);
            if (cartItem == null)
            {
                return Json(new { success = false, message = "Cart item not found" });
            }

            return Json(new { success = true, message = "Cart item deleted successfully" });
        }
        [HttpPost]
        public async Task<IActionResult> DeleteAllCartItems()
        {
            var userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return Json(new { success = false, message = "User not logged in." });
            }

            var cartItems = await _cartRepository.DeleteAllCartItems((int)userId);

            if (cartItems == null)
            {
                return Json(new { success = false, message = "No items to delete." });
            }
            return Json(new { success = true, message = "All cart items deleted successfully." });
        }

    }
}
