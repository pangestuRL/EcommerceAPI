using EcommerceAPI.Data;
using EcommerceAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EcommerceAPI.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly EcommerceContext _context;

        public CartRepository(EcommerceContext context)
        {
            _context = context;
        }

        public async Task<CartItem?> GetByIdAsync(int id)
        {
            return await _context.CartItems.Include(c => c.Product).FirstOrDefaultAsync(c => c.CartItemID == id);
        }

        public async Task<CartItem?> GetByUserAndProductAsync(int userId, int productId)
        {
            return await _context.CartItems.FirstOrDefaultAsync(c => c.UserID == userId && c.ProductID == productId);
        }

        public async Task<IEnumerable<CartItem>> GetByUserAsync(int userId)
        {
            return await _context.CartItems.Include(c => c.Product).Where(c => c.UserID == userId).ToListAsync();
        }

        public async Task AddAsync(CartItem item)
        {
            await _context.CartItems.AddAsync(item);
        }

        public async Task UpdateAsync(CartItem item)
        {
            _context.CartItems.Update(item);
        }

        public async Task DeleteAsync(CartItem item)
        {
            _context.CartItems.Remove(item);
        }

        public async Task ClearUserCartAsync(int userId)
        {
            var items = _context.CartItems.Where(c => c.UserID == userId);
            _context.CartItems.RemoveRange(items);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
