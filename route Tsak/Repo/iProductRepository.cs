using route_Tsak.Dto;
using route_Tsak.Models;

namespace route_Tsak.Repo
{
    public interface iProductRepository
    {
        Task AddAsync(Product product);
        Task <Product> GetById(int id);
        List<Product> GetAll();
        Task UpdateAsync(Product product);
        Task DeleteAsync(int id);
        Task SaveChangesAsync();
    }
}
