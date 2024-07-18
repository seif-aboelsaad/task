using Microsoft.AspNetCore.Http.HttpResults;
using route_Tsak.Dto;
using route_Tsak.Models;
using route_Tsak.Repo;

namespace route_Tsak.Services
{
    public class ProductServices : IProductServices
    {
        readonly iProductRepository _productRepository;
        public ProductServices(iProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        async Task IProductServices.AddProductAsync(ProductDto productDto)
        {
            await _productRepository.AddAsync(new Product
            {
                Name = productDto.Name,
                Price = productDto.Price,
                Stock = productDto.Stock
            });


        }

        async Task IProductServices.DeleteProductAsync(int id)
        {
           await _productRepository.DeleteAsync(id);
            
        }

        List<ProductDto> IProductServices.GetAllProducts()
        {
            var products = _productRepository.GetAll();
            List<ProductDto> productDtos = new List<ProductDto>();
            foreach (var product in products)
            {
                productDtos.Add(new ProductDto
                {
                    ProductId = product.ProductId,
                    Name = product.Name,
                    Price = product.Price,
                    Stock = product.Stock
                });
            }
            return productDtos;


        }

        public async Task<ProductDto> GetProductById(int id)
        {
            var product = await _productRepository.GetById(id);
            if (product == null)
            {
                return null;
            }

            return new ProductDto
            {
                ProductId = product.ProductId,
                Name = product.Name,
                Price = product.Price,
                Stock = product.Stock
            };
        }

        async Task IProductServices.UpdateProductAsync(ProductDto productDto)
        {
           await _productRepository.UpdateAsync(new Product
            {
                ProductId = productDto.ProductId,
                Name = productDto.Name,
                Price = productDto.Price,
                Stock = productDto.Stock
            });
        }

       
    }
}
