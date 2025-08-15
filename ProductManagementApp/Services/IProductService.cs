using ProductManagementApp.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProductManagementApp.Services
{
    public interface IProductService
    {
        Task<Product?> GetProductByIdAsync(Guid id);
        Task<(List<Product> Products, int TotalPages)> GetProductsAsync(string? filter, int pageNumber, int pageSize);
        Task AddProductAsync(Product product);
        Task<bool> UpdateProductAsync(Product product);
        Task DeleteProductAsync(Guid id);
    }
}