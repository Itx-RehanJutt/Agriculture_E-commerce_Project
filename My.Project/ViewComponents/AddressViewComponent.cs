using Infrastructure.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace My.Project.ViewComponents
{
    public class AddressViewComponent : ViewComponent
    {
        //private readonly IAddressRepository _addressRepository;

        //public AddressViewComponent(IAddressRepository addressRepository)
        //{
        //    _addressRepository = addressRepository;
        //}
        public async Task<IViewComponentResult> InvokeAsync(AddressCreateVM model)
        {
            return View("Address" ,model);
        }
    }
}
