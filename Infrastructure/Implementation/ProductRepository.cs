using DbEntities.Entities;
using Infrastructure.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.EntityFrameworkCore;
using My.Project.Areas.Admin.ViewModel;

namespace Infrastructure.Implementation
{
    public class ProductRepository : ICMSRepository
    {
        private readonly AgricultureProjectContext _context;

        public ProductRepository(AgricultureProjectContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<ProductVM>> Product()
        {
            var model = new List<ProductVM>();
            var products = await _context.Products.Where(p => p.IsDeleted != true).ToListAsync();
            if (products.Any())
            {
                foreach (var product in products)
                {
                    model.Add(new ProductVM()
                    {
                        Category =  _context.Categories.Where(p => p.CategoryId == product.CategoryId).FirstOrDefault()?.CategoryName,
                        ProductImage = product.ProductImage,
                        ProductName = product.ProductName,
                        ProductId = product.ProductId,
                        ProductDiscription = product.ProductDiscription,
                        Price = product.Price,
                        IsActive = product.IsActive
                    });
                }
            }
            return model;
        }
        public async Task<List<LookupVM>> CategoryGetAll()
        {
            var model = new List<LookupVM>();
            var categories = await _context.Categories.Where(p => p.IsDeleted != true).ToListAsync();
            if (categories.Any())
            {
                foreach (var C in categories)
                {
                    model.Add(new LookupVM()
                    {
                        Id = C.CategoryId,
                        Name = C.CategoryName,
                    });
                }
            }
            else
            {
                return null;
            }
                return model;
        }
        public async void AddProduct(ProductCreateVM model)
        {
            var entity = new Product()
            {
                CategoryId = model.CategoryId,
                ProductName = model.ProductName,
                ProductDiscription = model.ProductDiscription,
                Price = model.Price,
                IsActive = model.IsActive,
                IsDeleted = false,
                ProductImage = model.ImageName == null ? "-" : model.ImageName,
            };
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
        public async Task<ProductVM> ProductDetailsGetById(int Id)
        {
            var product = await _context.Products.Where(p => p.ProductId == Id && p.IsDeleted != true).FirstOrDefaultAsync();
            if (product == null)
            {
                return null;
            }
            var model = new ProductVM()
            {
                Category = _context.Categories.Where(p => p.CategoryId == product.CategoryId).FirstOrDefault()?.CategoryName,
                ProductName = product.ProductName,
                ProductDiscription = product.ProductDiscription,
                Price = product.Price,
                ProductImage = product.ProductImage,
                ProductId = Id,
                IsActive = product.IsActive,
                
            };
            return model;
        }
        public async Task<ProductUpdateVM> ProductGetById(int Id)
        {
            var product = await _context.Products.Where(p => p.ProductId == Id && p.IsDeleted != true).FirstOrDefaultAsync();
            if (product == null)
            {
                return null;
            }
            var model = new ProductUpdateVM()
            {
                CategoryId = product.CategoryId,
                ProductName = product.ProductName,
                ProductDiscription = product.ProductDiscription,
                Price = product.Price,
                ProductImageName = product.ProductImage,
                Id = Id,
                IsActive = product.IsActive,
                IsDeleted = product.IsDeleted,
                Categories = await CategoryGetAll()
            };
            return model;
        }
        public async Task<bool> ProductUpdate(ProductUpdateVM model)
        {
            var product = await _context.Products.Where(p => p.ProductId == model.Id && p.IsDeleted != true).FirstOrDefaultAsync();
            if (product == null)
            {
                return false;
            }
            product.Price = model.Price;
            product.ProductName = model.ProductName;
            product.ProductDiscription = model.ProductDiscription;
            product.CategoryId = model.CategoryId;
            product.IsActive = model.IsActive;
            if (model.ImageName != null)
            {
                product.ProductImage = model.ImageName;
            }
            _context.Update(product);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<int> ProductDelete(int id)
        {
            var product = await _context.Products.Where(p => p.ProductId == id && p.IsDeleted != true).FirstOrDefaultAsync();
            if (product != null)
            {
                product.IsDeleted = true;

                _context.Update(product);
                await _context.SaveChangesAsync();
                return 1;
            }
            else
            {
                return 0;
            }
        }
    }
}
