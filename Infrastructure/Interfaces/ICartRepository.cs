using DbEntities.Entities;
using Infrastructure.ViewModels;

namespace Infrastructure.Interfaces
{
    public interface ICartRepository
    {
        Task<CartItem> AddToCart(int loginId, int ProductId, int quantity);
        Task<IEnumerable<CartItemVM>> GetCartItemsByLoginId(int loginId);
        Task<CartItemUpdateVM> UpdateCart(CartItemUpdateVM model);
        //Task<List<CartItemUpdateVM>> UpdateCart(List<CartItemUpdateVM> models);
        Task<CartVM> GetCartItems(List<int> productIds = null);
        Task<int> GetCartCount(int loginId);
        Task<CartItem> DeleteCartItem(int productId);
        Task<List<CartItem>> DeleteAllCartItems(int loginId);
    }
}
