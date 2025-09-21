using EcommerceAPI.Models.DTO;

namespace EcommerceAPI.Services
{
    public interface ICartService
    {
        Task<bool> AddToCartAsync(int userId, int productId, int quantity);
        Task<bool> UpdateCartItemAsync(int userId, int cartItemId, UpdateCartRequest request); // ✅ pakai DTO
        Task<bool> RemoveFromCartAsync(int userId, int cartItemId);
        Task<IEnumerable<CartItemResponse>> GetCartAsync(int userId);
        Task<bool> CheckoutAsync(int userId);
    }
}
