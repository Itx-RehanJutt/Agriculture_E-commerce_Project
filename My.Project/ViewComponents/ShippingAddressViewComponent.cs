using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace My.Project.ViewComponents
{
    public class ShippingAddressViewComponent : ViewComponent
    {
        private readonly IAddressRepository _addressRepository;

        public ShippingAddressViewComponent(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(int customerId)
        {
            var address = await _addressRepository.GetAddressById(customerId);
            return View("ShippingAddress", address);
        }
    }

}
