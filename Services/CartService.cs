using EcommerceAPI.Models;
using EcommerceAPI.Models.DTO;
using EcommerceAPI.Repositories;

namespace EcommerceAPI.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepo;
        private readonly IProductRepository _productRepo;
        private readonly IOrderRepository _orderRepo;

        public CartService(ICartRepository cartRepo, IProductRepository productRepo, IOrderRepository orderRepo)
        {
            _cartRepo = cartRepo;
            _productRepo = productRepo;
            _orderRepo = orderRepo;
        }

        public async Task<bool> AddToCartAsync(int userId, int productId, int quantity)
        {
            var product = await _productRepo.GetByIdAsync(productId);
            if (product == null || product.Stock < quantity) return false;

            var cartItem = await _cartRepo.GetByUserAndProductAsync(userId, productId);
            if (cartItem != null)
            {
                cartItem.Quantity += quantity;
                await _cartRepo.UpdateAsync(cartItem);
            }
            else
            {
                await _cartRepo.AddAsync(new CartItem
                {
                    UserID = userId,
                    ProductID = productId,
                    Quantity = quantity
                });
            }
            await _cartRepo.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateCartItemAsync(int userId, int cartItemId, UpdateCartRequest request) // ✅ DTO
        {
            var cartItem = await _cartRepo.GetByIdAsync(cartItemId);
            if (cartItem == null || cartItem.UserID != userId) return false;

            // update quantity
            cartItem.Quantity = request.Quantity;
            await _cartRepo.UpdateAsync(cartItem);
            await _cartRepo.SaveChangesAsync();

            return true;
        }

        public async Task<bool> RemoveFromCartAsync(int userId, int cartItemId)
        {
            var cartItem = await _cartRepo.GetByIdAsync(cartItemId);
            if (cartItem == null || cartItem.UserID != userId) return false;

            await _cartRepo.DeleteAsync(cartItem);
            await _cartRepo.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<CartItemResponse>> GetCartAsync(int userId)
        {
            var cartItems = await _cartRepo.GetByUserAsync(userId);
            return cartItems.Select(ci => new CartItemResponse
            {
                CartItemID = ci.CartItemID,
                ProductID = ci.ProductID,
                ProductName = ci.Product?.ProductName ?? "",
                Quantity = ci.Quantity,
                Price = ci.Product?.Price ?? 0
            });
        }

        public async Task<bool> CheckoutAsync(int userId)
        {
            var cartItems = await _cartRepo.GetByUserAsync(userId);
            if (!cartItems.Any()) return false;

            var order = new Order
            {
                UserID = userId,
                OrderDate = DateTime.UtcNow,
                Status = "pending",
                TotalAmount = cartItems.Sum(ci => (ci.Product?.Price ?? 0) * ci.Quantity),
                OrderItems = cartItems.Select(ci => new OrderItem
                {
                    ProductID = ci.ProductID,
                    Quantity = ci.Quantity,
                    Price = ci.Product?.Price ?? 0
                }).ToList()
            };

            await _orderRepo.AddAsync(order);
            await _orderRepo.SaveChangesAsync();

            await _cartRepo.ClearUserCartAsync(userId);
            await _cartRepo.SaveChangesAsync();

            return true;
        }
    }
}
