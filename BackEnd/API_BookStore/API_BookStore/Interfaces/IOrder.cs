using API_BookStore.Models;

namespace API_BookStore.Interfaces
{
    public interface IOrder
    {
        Task<List<Order?>> GetOrdersAsync();
        Task<Order?> GetOrderByCodeAsync(string orderCode);
        Task<bool> CreateOrderAsync(Order order);
    }
}
