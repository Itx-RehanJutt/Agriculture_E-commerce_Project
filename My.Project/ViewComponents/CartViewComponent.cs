using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace My.Project.ViewComponents
{
    public class CartViewComponent : ViewComponent
    {
        private ICartRepository _cartRepository;

        public CartViewComponent(ICartRepository cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(List<int> productID)
        {
            var model = await _cartRepository.GetCartItems(productID);
            return View("Cart" , model);
        }
    }
}