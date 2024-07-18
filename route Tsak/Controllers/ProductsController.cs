using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using route_Tsak.Dto;
using route_Tsak.Services;

namespace route_Tsak.Controllers
{
    [Microsoft.AspNetCore.Mvc.Route("api/[controller]")]
    [ApiController]
    public class ProductsController
    {
        readonly IProductServices _productServices;
        public ProductsController(IProductServices productServices)
        {
            _productServices = productServices;
        }
        [HttpGet]
        public List<ProductDto> Get()
        {
            return _productServices.GetAllProducts();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ProductDto>> GetProduct(int id)
        {
            var product = await _productServices.GetProductById(id);
            if (product == null)
            {
                return null;
            }
            return product;
        }
        [HttpPost]
        public Task Post([FromBody] ProductDto productDto)
        {
            return _productServices.AddProductAsync(productDto);
        }
        [HttpPut("{id}")]
        public Task Put(int id, [FromBody] ProductDto productDto)
        {
            productDto.ProductId = id;
            return _productServices.UpdateProductAsync(productDto);
        }
        [HttpDelete("{id}")]
        public Task Delete(int id)
        {
            return _productServices.DeleteProductAsync(id);
        }

    }
}
