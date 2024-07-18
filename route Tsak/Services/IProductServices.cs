using route_Tsak.Dto;

namespace route_Tsak.Services
{
    public interface IProductServices
    {
        List<ProductDto> GetAllProducts();
        Task<ProductDto> GetProductById(int id);
        Task AddProductAsync(ProductDto productDto);
        Task UpdateProductAsync(ProductDto productDto);

        Task DeleteProductAsync(int id);


    }
}
