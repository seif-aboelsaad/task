using Microsoft.EntityFrameworkCore;
using route_Tsak.Models;
using route_Tsak.Repo;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace route_Tsak.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TaskDbContext _context;

        public CustomerRepository(TaskDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(ApplicationUser customer)
        {
            _context.Customers.Add(customer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ApplicationUser customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ApplicationUser customer)
        {
            _context.Customers.Remove(customer);
            await _context.SaveChangesAsync();
        }

        public List<ApplicationUser> GetAll()
        {
            return _context.Customers.ToList();
        }

        public ApplicationUser GetById(string id)
        {
            return _context.Customers.FirstOrDefault(c => c.Id == id);
        }

    }
}
