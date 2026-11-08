using Infrastructure.ViewModels;
using My.Project.Areas.Admin.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces
{
    public interface ICMSRepository
    {
        Task<IEnumerable<ProductVM>> Product();
        Task<List<LookupVM>> CategoryGetAll();
        void AddProduct(ProductCreateVM model);
        Task<ProductVM> ProductDetailsGetById(int Id);
        Task<ProductUpdateVM> ProductGetById(int Id);
        Task<bool> ProductUpdate(ProductUpdateVM model);
        Task<int> ProductDelete(int id);
    }
}
