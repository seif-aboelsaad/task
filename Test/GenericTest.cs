using Microsoft.EntityFrameworkCore;
using route_Tsak.Models;
using route_Tsak.Repo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    public class GenericTest
    {
        public class ProductRepositoryTests
        {
            private DbContextOptions<TaskDbContext> GetInMemoryDbContextOptions(string dbName)
            {
                return new DbContextOptionsBuilder<TaskDbContext>()
                    .UseInMemoryDatabase(databaseName: dbName)
                    .Options;
            }

            [Fact]
            public async Task AddAsync_ShouldAddProduct()
            {
                // Arrange
                var options = GetInMemoryDbContextOptions("AddAsync_ShouldAddProduct");
                using var context = new TaskDbContext(options);
                var repository = new ProductRepository(context);
                var product = new Product { ProductId = 1000, Name = "Test Product", Price = 10.99m, Stock = 100 };

                // Act
                await repository.AddAsync(product);
                var result = await context.Products.FindAsync(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Test Product", result.Name);
            }

            [Fact]
            public async Task GetById_ShouldReturnProduct()
            {
                // Arrange
                var options = GetInMemoryDbContextOptions("GetById_ShouldReturnProduct");
                using var context = new TaskDbContext(options);
                var product = new Product { ProductId =20000, Name = "Test Product", Price = 10.99m, Stock = 100 };
                context.Products.Add(product);
                context.SaveChanges();
                var repository = new ProductRepository(context);

                // Act
                var result = await repository.GetById(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Test Product", result.Name);
            }

            [Fact]
            public async Task UpdateAsync_ShouldUpdateProduct()
            {
                // Arrange
                var options = GetInMemoryDbContextOptions("UpdateAsync_ShouldUpdateProduct");
                using var context = new TaskDbContext(options);
                var product = new Product { ProductId = 80, Name = "Test Product", Price = 10.99m, Stock = 100 };
                context.Products.Add(product);
                context.SaveChanges();
                var repository = new ProductRepository(context);

                // Act
                product.Name = "Updated Product";
                await repository.UpdateAsync(product);
                var result = await context.Products.FindAsync(1);

                // Assert
                Assert.NotNull(result);
                Assert.Equal("Updated Product", result.Name);
            }

            [Fact]
            public async Task DeleteAsync_ShouldRemoveProduct()
            {
                // Arrange
                var options = GetInMemoryDbContextOptions("DeleteAsync_ShouldRemoveProduct");
                using var context = new TaskDbContext(options);
                var product = new Product { ProductId = 1111, Name = "Test Product", Price = 10.99m, Stock = 100 };
                context.Products.Add(product);
                context.SaveChanges();
                var repository = new ProductRepository(context);

                // Act
                await repository.DeleteAsync(1111);
                var result = await context.Products.FindAsync(1);

                // Assert
                Assert.Null(result);
            }

            //[Fact]
            //public void GetAll_ShouldReturnAllProducts()
            //{
            //    // Arrange
            //    var options = GetInMemoryDbContextOptions("GetAll_ShouldReturnAllProducts");
            //    using var context = new TaskDbContext(options);
            //    var product1 = new Product { ProductId = 1, Name = "Test Product 1", Price = 10.99m, Stock = 100 };
            //    var product2 = new Product { ProductId = 2, Name = "Test Product 2", Price = 20.99m, Stock = 200 };
            //    context.Products.AddRange(product1, product2);
            //    context.SaveChanges();
            //    var repository = new ProductRepository(context);

            //    // Act
            //    var result = repository.GetAll();

            //    // Assert
            //    Assert.Equal(2, result.Count);
            //}
        }
    }
}
