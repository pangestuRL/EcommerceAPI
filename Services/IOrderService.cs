using EcommerceAPI.Models.DTO;

namespace EcommerceAPI.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderResponse>> GetUserOrdersAsync(int userId);
        Task<IEnumerable<OrderResponse>> GetAllOrdersAsync();
        Task<OrderResponse?> GetOrderByIdAsync(int id);
        Task<bool> UpdateOrderStatusAsync(int id, UpdateOrderStatusRequest request);
    }
}
