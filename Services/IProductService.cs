using EcommerceAPI.Models.DTO;

namespace EcommerceAPI.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductResponse>> GetAllProductsAsync();
        Task<ProductResponse?> GetProductByIdAsync(int id);
        Task<ProductResponse> CreateProductAsync(ProductRequest request);
        Task<bool> UpdateProductAsync(int id, ProductRequest request);
        Task<bool> DeleteProductAsync(int id);
    }
}
