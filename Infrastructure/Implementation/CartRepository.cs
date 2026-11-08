using Azure.Core;
using DbEntities.Entities;
using Infrastructure.Interfaces;
using Infrastructure.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

public class CartRepository : ICartRepository
{
    private readonly AgricultureProjectContext _context;

    public CartRepository(AgricultureProjectContext context)
    {
        _context = context;
    }

    public async Task<CartItem> AddToCart(int loginId, int ProductId, int quantity)
    {
        var cart = await _context.Carts.Include(c => c.CartItems).Where(c => c.LoginId == loginId).FirstOrDefaultAsync();
        if (cart == null)
        {
            cart = new Cart()
            {
                LoginId = loginId,
                TotalPrice = 0,
            };
            await _context.AddAsync(cart);
            await _context.SaveChangesAsync();
        }
        var cartItem = await _context.CartItems.Where(ci => ci.ProductId == ProductId && ci.CartId == cart.CartId).FirstOrDefaultAsync();
        if (cartItem == null)
        {
            cartItem = new CartItem()
            {
                ProductId = ProductId,
                CartId = cart.CartId,
                Quantity = quantity
            };
            await _context.AddAsync(cartItem);
            await _context.SaveChangesAsync();
        }
        else
        {
            cartItem.Quantity += quantity;
        }
        await UpdateTotalPrice(cart.CartId);
        return cartItem;
    }

    private async Task UpdateTotalPrice(int cartId)
    {
        var cart = await _context.Carts.Include(c => c.CartItems).ThenInclude(ci => ci.Product).Where(c => c.CartId == cartId).FirstOrDefaultAsync();
        if (cart == null)
        {
            throw new Exception($"Cart with ID {cartId} not found.");
        }
        cart.TotalPrice = cart.CartItems.Sum(ci => ci.Quantity * ci.Product?.Price ?? 0);
        _context.Update(cart);
        await _context.SaveChangesAsync();
    }
    public async Task<IEnumerable<CartItemVM>> GetCartItemsByLoginId(int loginId)
    {
        var model = new List<CartItemVM>();
        var cartItems = await _context.CartItems.Include(ci => ci.Product).Where(ci => ci.Cart != null && ci.Cart.LoginId == loginId).ToListAsync();
        if (cartItems.Any())
        {
            foreach (var item in cartItems)
            {
                model.Add(new CartItemVM()
                {
                    CartId = item.CartId,
                    cartItemId = item.CartItemId,
                    ProductId = item.ProductId,
                    ProductName = item.Product?.ProductName,
                    ProductImage = item.Product?.ProductImage,
                    Quantity = item.Quantity,
                    Price = item.Product?.Price
                });
            }
        }
        return model;
    }
    public async Task<CartVM> GetCartItems(List<int> productIds = null)
    {
     // Agar productIds null hai, toh total zero return kar do
    if (productIds == null || !productIds.Any())
        {
            return new CartVM
            {
                Quantity = 0,
                TotalPrice = 0
            };
        }
        var selectedCartItems = await _context.CartItems
            .Include(ci => ci.Product)
            .Where(ci => productIds.Contains((int)ci.ProductId))
            .ToListAsync();

        // Calculate Total Quantity and Total Price
        var totalQuantity = selectedCartItems.Sum(item => item.Quantity);
        var totalPrice = selectedCartItems.Sum(item => item.Quantity * (item.Product?.Price ?? 0));

        return new CartVM()
        {
            Quantity = (decimal)totalQuantity,
            TotalPrice = (decimal)totalPrice
        };
    }

    //public async Task<CartItemUpdateVM> UpdateCart(CartItemUpdateVM model)
    //{
    //    // Get the specific cart item by ProductId
    //    var cartItem = await _context.CartItems
    //        .Include(ci => ci.Product)
    //        .FirstOrDefaultAsync(ci => ci.ProductId == model.ProductId);

    //    if (cartItem == null)
    //    {
    //        return null; // Item not found
    //    }

    //    // Calculate the old item's total price
    //    var oldItemTotalPrice = cartItem.Quantity * (cartItem.Product?.Price ?? 0);

    //    cartItem.Quantity = model.Quantity;
    //    _context.Update(cartItem.Quantity);

    //    var cart = await _context.Carts
    //        .Include(c => c.CartItems)
    //        .ThenInclude(ci => ci.Product) // Ensure Products are included
    //        .FirstOrDefaultAsync(c => c.CartId == cartItem.CartId);

    //    if (cart == null)
    //    {
    //        throw new Exception($"Cart with ID {cartItem.CartId} not found.");
    //    }

    //    // Calculate the new item's total price
    //    var newItemTotalPrice = cartItem.Quantity * (cartItem.Product?.Price ?? 0);
    //    if (cart.TotalPrice > 0)
    //    {
    //        cart.TotalPrice = (decimal)(cart.TotalPrice - oldItemTotalPrice + newItemTotalPrice);
    //    }
    //    else
    //    {
    //        // If the cart total price is zero, directly add the new item's total price
    //        cart.TotalPrice = (decimal)newItemTotalPrice;
    //    }
    //    //var ItemTotalPrice = cartItem.Quantity * (cartItem.Product?.Price ?? 0);
    //    _context.Update(cart.TotalPrice);
    //    await _context.SaveChangesAsync();

    //    return new CartItemUpdateVM
    //    {
    //        Quantity = (int)cartItem.Quantity,
    //        TotalPrice = cart.TotalPrice
    //    };
    //}
    public async Task<CartItemUpdateVM> UpdateCart(CartItemUpdateVM model)
    {
        var cartItem = await _context.CartItems
            .Include(ci => ci.Product)
            .FirstOrDefaultAsync(ci => ci.ProductId == model.ProductId);

        if (cartItem == null)
        {
            return null; // Item not found
        }

        var oldItemTotalPrice = cartItem.Quantity * (cartItem.Product?.Price ?? 0);

        cartItem.Quantity = model.Quantity;
        _context.CartItems.Update(cartItem);

        var newItemTotalPrice = cartItem.Quantity * (cartItem.Product?.Price ?? 0);

        var cart = await _context.Carts.FirstOrDefaultAsync(c => c.CartId == cartItem.CartId);
        if (cart == null)
        {
            throw new Exception($"Cart with ID {cartItem.CartId} not found.");
        }
        if (cart.TotalPrice > 0)
        {
            cart.TotalPrice = (decimal)(cart.TotalPrice - oldItemTotalPrice + newItemTotalPrice);
        }
        else
        {
            // If the cart total price is zero, directly add the new item's total price
            cart.TotalPrice = (decimal)newItemTotalPrice;
        }
        var ItemTotalPrice = cartItem.Quantity * (cartItem.Product?.Price ?? 0);
        _context.Carts.Update(cart);
        await _context.SaveChangesAsync();

        return new CartItemUpdateVM
        {
            ProductId = (int)cartItem.ProductId,
            Quantity = (int)cartItem.Quantity,
            TotalPrice = cart?.TotalPrice ?? 0
        };
    }

    public async Task<int> GetCartCount(int loginId)
    {
        return await _context.CartItems
            .Where(ci => ci.CartId == _context.Carts
                .Where(c => c.LoginId == loginId)
                .Select(c => (int?)c.CartId)
                .FirstOrDefault())
            .SumAsync(ci => (int)ci.Quantity);
    }


    public async Task<CartItem> DeleteCartItem(int productId)
    {
        var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.ProductId == productId);
        if (cartItem == null)
        {
            return null;
        }
        _context.CartItems.Remove(cartItem);
        await _context.SaveChangesAsync();
        return cartItem;
    }
    public async Task<List<CartItem>> DeleteAllCartItems(int loginId)
    {
        var cart = await _context.Carts.Include(c => c.CartItems).Where(c => c.LoginId == loginId).FirstOrDefaultAsync();
        if (cart == null)
        {
            return null;
        }
        var deleteItem = cart.CartItems.ToList();
        _context.CartItems.RemoveRange(cart.CartItems);
        await _context.SaveChangesAsync();
        return deleteItem;
    }
    //private async Task<decimal> GetProductPrice(int productId)
    //{
    //    var product = await _context.Products.Where(p => p.ProductId == productId).FirstOrDefaultAsync();
    //    if (product == null)
    //    {
    //        throw new Exception($"Product with ID {productId} not found.");
    //    }
    //    return product.Price;
    //}
}
