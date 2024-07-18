using route_Tsak.Dto;
using route_Tsak.Models;

namespace route_Tsak.Repo
{
    public class ProductRepository : iProductRepository
    {
        readonly TaskDbContext _context;
        public ProductRepository(TaskDbContext context)
        {

            _context = context;
        }
        public async Task AddAsync(Product product)
        {
            _context.Products.Add(product);
            await _context.SaveChangesAsync();


        }

        public async Task DeleteAsync(int id)
        {
            _context.Products.Remove(_context.Products.Find(id));
            await _context.SaveChangesAsync();
        }

        List<Product> iProductRepository.GetAll()
        {
            return _context.Products.ToList();
        }

        
          public async Task<Product> GetById(int id)
        {
            return _context.Products.FirstOrDefault(data => data.ProductId == id);


        }

        Task iProductRepository.SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }

         public async Task UpdateAsync(Product product)
        {
            var productToUpdate = _context.Products.Find(product.ProductId);
            productToUpdate.Name = product.Name;
            productToUpdate.Price = product.Price;
            productToUpdate.Stock = product.Stock;
            await _context.SaveChangesAsync();
        }

       
    }
}
