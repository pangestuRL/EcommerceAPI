using EcommerceAPI.Models.DTO;
using EcommerceAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EcommerceAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "customer")]
    public class CartController : ControllerBase
    {
        private readonly ICartService _service;

        public CartController(ICartService service)
        {
            _service = service;
        }

        private int GetUserId() => int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

        [HttpGet]
        public async Task<IActionResult> GetCart()
        {
            var userId = GetUserId();
            var cart = await _service.GetCartAsync(userId);
            return Ok(cart);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromBody] AddToCartRequest request)
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

            var result = await _service.AddToCartAsync(userId, request.ProductID, request.Quantity);
            if (!result) return BadRequest("Gagal menambahkan produk ke cart");

            return Ok("Produk berhasil ditambahkan ke cart");
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCartItem(int id, [FromBody] UpdateCartRequest request)
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value ?? "0");

            var result = await _service.UpdateCartItemAsync(userId, id, request);
            if (!result) return NotFound("Item tidak ditemukan di cart");

            return Ok("Cart berhasil diupdate");
        }


        [HttpDelete("{cartItemId}")]
        public async Task<IActionResult> RemoveFromCart(int cartItemId)
        {
            var userId = GetUserId();
            var success = await _service.RemoveFromCartAsync(userId, cartItemId);
            if (!success) return NotFound();
            return Ok(new { message = "Item removed from cart" });
        }

        [HttpPost("checkout")]
        public async Task<IActionResult> Checkout()
        {
            var userId = GetUserId();
            var success = await _service.CheckoutAsync(userId);
            if (!success) return BadRequest(new { message = "Cart is empty" });
            return Ok(new { message = "Order created successfully" });
        }
    }
}
