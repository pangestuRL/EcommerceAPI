using EcommerceAPI.Models;
using EcommerceAPI.Models.DTO;
using EcommerceAPI.Repositories;

namespace EcommerceAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ProductResponse>> GetAllProductsAsync()
        {
            var products = await _repository.GetAllAsync();
            return products.Select(p => new ProductResponse
            {
                ProductID = p.ProductID,
                ProductName = p.ProductName,
                Description = p.Description,
                Price = p.Price,
                Stock = p.Stock,
                Category = p.Category
            });
        }

        public async Task<ProductResponse?> GetProductByIdAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return null;

            return new ProductResponse
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Category = product.Category
            };
        }

        public async Task<ProductResponse> CreateProductAsync(ProductRequest request)
        {
            var product = new Product
            {
                ProductName = request.ProductName,
                Description = request.Description,
                Price = request.Price,
                Stock = request.Stock,
                Category = request.Category,
                CreatedAt = DateTime.UtcNow
            };

            await _repository.AddAsync(product);
            await _repository.SaveChangesAsync();

            return new ProductResponse
            {
                ProductID = product.ProductID,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Stock = product.Stock,
                Category = product.Category
            };
        }

        public async Task<bool> UpdateProductAsync(int id, ProductRequest request)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return false;

            product.ProductName = request.ProductName;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Stock = request.Stock;
            product.Category = request.Category;
            product.UpdatedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(product);
            await _repository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteProductAsync(int id)
        {
            var product = await _repository.GetByIdAsync(id);
            if (product == null) return false;

            await Task.Run(() => _repository.DeleteAsync(product));
            await _repository.SaveChangesAsync();
            return true;
        }
    }
}
