using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using route_Tsak.Models;
using route_Tsak.Repo;
using route_Tsak.Services;
using Xunit;

namespace Test
{
    public class ProductServiceTests
    {
        [Fact]
        public async Task GetProductById_ReturnsProductDto()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<TaskDbContext>()
                          .UseInMemoryDatabase(databaseName: "Test_GetProductById")
                          .Options;

            using (var context = new TaskDbContext(options))
            {
                // Seed the in-memory database with test data
                context.Products.Add(new Product
                {
                    ProductId = 1,
                    Name = "Test Product",
                    Price = 10.99m,
                    Stock = 100
                });
                context.SaveChanges();
            }

            using (var context = new TaskDbContext(options))
            {
                var repository = new ProductRepository(context);
                var productService = new ProductServices(repository);

                // Act
                var result = await productService.GetProductById(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal(1, result.ProductId);
                Assert.Equal("Test Product", result.Name);
                Assert.Equal(10.99m, result.Price);
                Assert.Equal(100, result.Stock);
            }
        }
    }
}
