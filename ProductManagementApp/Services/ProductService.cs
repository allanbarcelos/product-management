using Microsoft.EntityFrameworkCore;
using ProductManagementApp.Data;
using ProductManagementApp.Models;

namespace ProductManagementApp.Services
{
    public class ProductService(AppDbContext context) : IProductService
    {
        private readonly AppDbContext _context = context;

        public async Task AddProductAsync(Product product)
        {
            ArgumentNullException.ThrowIfNull(product); // Ensure product is not null

            product.CreatedAt = DateTime.UtcNow;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteProductAsync(Guid id)
        {
            ArgumentNullException.ThrowIfNull(id, nameof(id)); // Ensure id is not null

            var product = _context.Products.Find(id);
            if (product == null)
            {
                throw new KeyNotFoundException($"Product with ID {id} not found.");
            }

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
        }

        public async Task<Product?> GetProductByIdAsync(Guid id)
        {
            return await _context.FindAsync<Product>(id);
        }

        public async Task<(List<Product> Products, int TotalPages)> GetProductsAsync(string? filter, int pageNumber, int pageSize)
        {
            ArgumentNullException.ThrowIfNull(pageNumber, nameof(pageNumber)); // Ensure pageNumber is not null
            ArgumentNullException.ThrowIfNull(pageSize, nameof(pageSize)); // Ensure pageSize is not null

            IQueryable<Product> query = _context.Products;

            if (!string.IsNullOrWhiteSpace(filter))
            {
                query = query.Where(p => p.Name.Contains(filter) || p.Description.Contains(filter));
            }

            int totalCount = await query.CountAsync();
            int totalPages = (int)Math.Ceiling((double)totalCount / pageSize);

            var products = await query
                .OrderByDescending(p => p.CreatedAt)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (products, totalPages);
        }

        public async Task<bool> UpdateProductAsync(Product product)
        {
            ArgumentNullException.ThrowIfNull(product); // Ensure product is not null

            var existingProduct = await _context.Products.FindAsync(product.Id);
            if (existingProduct == null)
            {
                throw new KeyNotFoundException($"Product with ID {product.Id} not found.");
            }

            existingProduct.Name = product.Name;
            existingProduct.Description = product.Description;
            existingProduct.Price = product.Price;

            _context.Products.Update(existingProduct);
            return await _context.SaveChangesAsync() > 0; // return true if any changes were made
        }
    }
}