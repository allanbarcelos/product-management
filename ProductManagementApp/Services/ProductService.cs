using Microsoft.EntityFrameworkCore;
using ProductManagementApp.Models;

namespace ProductManagementApp.Services
{
    public class ProductService : IProductService
    {
        public Task AddProductAsync(Product product)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Product?> GetProductByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<(List<Product> Products, int TotalPages)> GetProductsAsync(string? filter, int pageNumber, int pageSize)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateProductAsync(Product product)
        {
            throw new NotImplementedException();
        }
    }
}