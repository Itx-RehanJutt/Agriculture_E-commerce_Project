using Infrastructure.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface IAddressRepository
    {
        Task<IEnumerable<AddressVM>> Address();
        Task AddAddress(AddressCreateVM model);
        Task<AddressUpdateVM> GetAddressById(int Customerid);
    }
}
