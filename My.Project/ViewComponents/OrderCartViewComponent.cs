using Infrastructure.Implementation;
using Infrastructure.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace My.Project.ViewComponents
{
    public class OrderCartViewComponent : ViewComponent
    {
        private IOrderRepository _orderRepository;

        public OrderCartViewComponent(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            
            string modelJson = HttpContext.Session.GetString("OrderModel");
            var orderModel = JsonConvert.DeserializeObject<OrderCartVM>(modelJson);
            return View("OrderCart", orderModel);
        }
    }
}