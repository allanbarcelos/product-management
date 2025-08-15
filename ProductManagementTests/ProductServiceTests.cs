using System;
using Microsoft.EntityFrameworkCore;
using ProductManagementApp.Data;
using ProductManagementApp.Models;
using ProductManagementApp.Services;
using Xunit;
using System.Linq;

namespace ProductManagement.ProductManagementTests
{
    public class ProductServiceTests
    {
        private AppDbContext CreateDbContext()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            return new AppDbContext(options);
        }

        [Fact]
        public async Task AddProductAsync_Should_Add_Product()
        {
            // Arrange
            using var context = CreateDbContext();
            var service = new ProductService(context);
            var product = new Product { Id = Guid.NewGuid(), Name = "Test Product", Description = "Desc", Price = 10.99m };

            // Act
            await service.AddProductAsync(product);

            // Assert
            var addedProduct = await context.Products.FirstOrDefaultAsync(p => p.Name == "Test Product");
            Assert.NotNull(addedProduct);
            Assert.Equal(10.99m, addedProduct.Price);
        }

        [Fact]
        public async Task GetProductByIdAsync_Should_Return_Product_When_Exists()
        {
            // Arrange
            using var context = CreateDbContext();
            var service = new ProductService(context);
            var product = new Product { Id = Guid.NewGuid(), Name = "Test Product", Description = "Desc", Price = 10.99m };
            await service.AddProductAsync(product);

            // Act
            var result = await service.GetProductByIdAsync(product.Id);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Test Product", result.Name);
        }

        [Fact]
        public async Task GetProductByIdAsync_Should_Filter_And_Paginate()
        {
            // Arrange
            using var context = CreateDbContext();
            var service = new ProductService(context);
            for (int i = 0; i < 50; i++)
            {
                await service.AddProductAsync(new Product { Id = Guid.NewGuid(), Name = $"Product {i}", Description = "Desc", Price = i });
            }

            // Act
            var (products, totalPages) = await service.GetProductsAsync("Product", 1, 10);

            // Assert
            Assert.Equal(10, products.Count);
            Assert.Equal(5, totalPages); // 50 products / 10 per page
        }


        [Fact]
        public async Task UpdateProductAsync_Should_Update_Existing_Product()
        {
            // Arrange
            using var context = CreateDbContext();
            var service = new ProductService(context);
            var product = new Product { Id = Guid.NewGuid(), Name = "Test Product", Description = "Desc", Price = 10.99m };
            await service.AddProductAsync(product);

            // Act
            product.Name = "Updated Product";
            product.Price = 15.99m;
            product.Description = "Updated Desc";
            var result = await service.UpdateProductAsync(product);

            // Assert
            Assert.True(result);
            var updatedProduct = await context.Products.FindAsync(product.Id);
            Assert.Equal("Updated Product", updatedProduct.Name);
            Assert.Equal(15.99m, updatedProduct.Price);
            Assert.Equal("Updated Desc", updatedProduct.Description);
        }

        [Fact]
        public async Task DeleteProductAsync_Should_Remove_Product()
        {
            // Arrange
            using var context = CreateDbContext();
            var service = new ProductService(context);
            var product = new Product { Id = Guid.NewGuid(), Name = "Test Product", Description = "Desc", Price = 10.99m };
            await service.AddProductAsync(product);

            // Act
            await service.DeleteProductAsync(product.Id);

            // Assert
            var deletedProduct = await context.Products.FindAsync(product.Id);
            Assert.Null(deletedProduct);
        }
    }
}