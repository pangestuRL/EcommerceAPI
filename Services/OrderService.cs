using EcommerceAPI.Models.DTO;
using EcommerceAPI.Repositories;

namespace EcommerceAPI.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepo;

        public OrderService(IOrderRepository orderRepo)
        {
            _orderRepo = orderRepo;
        }

        public async Task<IEnumerable<OrderResponse>> GetUserOrdersAsync(int userId)
        {
            var orders = await _orderRepo.GetByUserAsync(userId);
            return orders.Select(MapToResponse);
        }

        public async Task<IEnumerable<OrderResponse>> GetAllOrdersAsync()
        {
            var orders = await _orderRepo.GetAllAsync();
            return orders.Select(MapToResponse);
        }

        public async Task<OrderResponse?> GetOrderByIdAsync(int id)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            return order == null ? null : MapToResponse(order);
        }

        public async Task<bool> UpdateOrderStatusAsync(int id, UpdateOrderStatusRequest request)
        {
            var order = await _orderRepo.GetByIdAsync(id);
            if (order == null) return false;

            order.Status = request.Status;
            await _orderRepo.UpdateAsync(order);
            await _orderRepo.SaveChangesAsync();

            return true;
        }

        private OrderResponse MapToResponse(Models.Order order)
        {
            return new OrderResponse
            {
                OrderID = order.OrderID,
                OrderDate = order.OrderDate,
                Status = order.Status,
                TotalAmount = order.TotalAmount,
                Items = order.OrderItems.Select(oi => new OrderItemResponse
                {
                    OrderItemID = oi.OrderItemID,
                    ProductID = oi.ProductID,
                    ProductName = oi.Product?.ProductName ?? string.Empty,
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            };
        }
    }
}
