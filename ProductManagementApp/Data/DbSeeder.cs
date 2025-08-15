using Bogus;
using ProductManagementApp.Models;

namespace ProductManagementApp.Data
{
    public static class DbSeeder
    {
        public static void Seed(AppDbContext context)
        {
            context.Database.EnsureCreated();

            if (context.Products.Any())
            {
                return; // DB has been seeded
            }

            var faker = new Faker<Product>()
                .RuleFor(p => p.Name, f => f.Commerce.ProductName())
                .RuleFor(p => p.Description, f => f.Commerce.ProductDescription())
                .RuleFor(p => p.Price, f => decimal.Parse(f.Commerce.Price(10, 1000)))
                .RuleFor(p => p.CreatedAt, f => f.Date.Past(1));

            var products = faker.Generate(50);
            
            context.Products.AddRange(products);
            context.SaveChanges();
        }
    }
}