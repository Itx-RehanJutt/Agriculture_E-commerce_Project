using Infrastructure.ViewModels;

namespace Infrastructure.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<OrderVM>> Order();
        Task AddOrder(OrderCartVM model);
        Task<OrderUpdateVM> GetOrderById(int id);
        Task<bool> UpdateOrder(OrderUpdateVM model);
        Task<int> DeleteOrder(int id);
        Task<OrderCartVM> OrderCart(int productId, int quantity, int CustomerId);
    }
}
