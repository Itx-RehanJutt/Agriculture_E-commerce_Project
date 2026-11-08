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
    public class AddressRepository : IAddressRepository
    {
        private AgricultureProjectContext _context;

        public AddressRepository(AgricultureProjectContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<AddressVM>> Address()
        {
            var model = new List<AddressVM>();
            var orders = await _context.CustomerAddresses.Where(p => p.IsDeleted != true).ToListAsync();
            if (orders.Any())
            {
                foreach (var order in orders)
                {
                    model.Add(new AddressVM()
                    {
                        City = order.City,
                        OrderAddress = order.Address,
                        OrderDate = order.CreateDate,
                        //IsActive = order.IsActive,
                        IsDeleted = order.IsDeleted
                    });
                }
            }
            return model;
        }
        public async Task AddAddress(AddressCreateVM model)
        {
            var order = new CustomerAddress()
            {
                City = model.City,
                Address = model.OrderAddress,
                Email = model.Email,
                FullName = model.Name,
                PhoneNo = model.PhoneNo,
                CustomerId = model.CustomerID,
                IsActive = true,
            };
            await _context.AddAsync(order);
            await _context.SaveChangesAsync();
        }
        public async Task<AddressUpdateVM> GetAddressById(int Customerid)
        {
            var order = await _context.CustomerAddresses.Where(p => p.CustomerId == Customerid && p.IsDeleted == false).FirstOrDefaultAsync();
            if (order != null)
            {
                return new AddressUpdateVM()
                {
                    City = order.City,
                    OrderAddress = order.Address,
                    Email = order.Email,
                    Name = order.FullName,
                    PhoneNo = order.PhoneNo,
                    CustomerId = order.CustomerId,
                };
            }
            return null;
        }
        //public async Task Address(int CustomerID)
        //{
        //    var address = await _context.CustomerAddresses.Where(p => p.CustomerId == CustomerID && p.IsDeleted != true).FirstOrDefaultAsync();
        //    if (address != null)
        //    {
        //        return address;
        //    }
        //}
    }
}
