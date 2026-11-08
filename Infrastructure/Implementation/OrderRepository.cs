using DbEntities.Entities;
using Infrastructure.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Implementation
{
    public class OrderRepository : IOrderRepository
    {
        private AgricultureProjectContext _context;

        public OrderRepository(AgricultureProjectContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<OrderVM>> Order()
        {
            var model = new List<OrderVM>();
            var orders = await _context.Orders.Where(p => p.IsDeleted != true).ToListAsync();
            if (orders.Any())
            {
                foreach (var order in orders)
                {
                    model.Add(new OrderVM()
                    {
                    });
                }
            }
            return model;
        }
        public async Task AddOrder(OrderCartVM model)
        {
            var order = new Order()
            {
                CustomerId = model.CustomerId,
                ProductId = model.ProductId,
                OrderQuantity = model.Quantity,
                TotalPrice = model.TotalPrice
            };
            await _context.AddAsync(order);
            await _context.SaveChangesAsync();
        }
        public async Task<OrderUpdateVM> GetOrderById(int id)
        {
            var order = await _context.CustomerAddresses.Where(p => p.AddressId == id && p.IsDeleted == false).FirstOrDefaultAsync();
            if (order != null)
            {
                return new OrderUpdateVM()
                {
                    Id = order.AddressId,
                    City = order.City,
                    OrderAddress = order.Address,
                    UserId = order.CustomerId,
                };
            }
            return null;
        }
        public async Task<bool> UpdateOrder(OrderUpdateVM model)
        {
            var order = await _context.CustomerAddresses.Where(p => p.AddressId == model.Id && p.IsDeleted == false).FirstOrDefaultAsync();
            if (order == null)
            {
                return false;
            }
            order.City = model.City;
            order.Address = model.OrderAddress;
            //order.CustomerId = model.UserId;
            _context.Update(order);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<int> DeleteOrder(int id)
        {
            var order = await _context.Orders.Where(p => p.OrderId == id && p.IsDeleted == false).FirstOrDefaultAsync();
            if (order == null)
            {
                return 0;
            }
            order.IsDeleted = true;
            _context.Update(order);
            await _context.SaveChangesAsync();
            return 1;
        }

        public async Task<OrderCartVM> OrderCart(int productId, int quantity, int CustomerId)
        {
            var cart = await _context.Products.Where(ci => ci.ProductId == productId).FirstOrDefaultAsync();
            if (cart == null)
            {
                return new OrderCartVM
                {
                    CustomerId = CustomerId,
                    ProductId = productId,
                    Quantity = 0,
                    TotalPrice = 0
                };
            }
            var model = new OrderCartVM()
            {
                ProductId = productId,
                CustomerId = CustomerId,
                Quantity = quantity,
                TotalPrice = quantity * (cart?.Price ?? 0)
            };
            return model;
        }
    }
}
