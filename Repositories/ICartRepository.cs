using EcommerceAPI.Models;

namespace EcommerceAPI.Repositories
{
    public interface ICartRepository
    {
        Task<CartItem?> GetByIdAsync(int id);
        Task<CartItem?> GetByUserAndProductAsync(int userId, int productId);
        Task<IEnumerable<CartItem>> GetByUserAsync(int userId);
        Task AddAsync(CartItem item);
        Task UpdateAsync(CartItem item);
        Task DeleteAsync(CartItem item);
        Task ClearUserCartAsync(int userId);
        Task SaveChangesAsync();
    }
}
