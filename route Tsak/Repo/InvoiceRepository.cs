using Microsoft.EntityFrameworkCore;
using route_Tsak.Models;

namespace route_Tsak.Repo
{
    public class InvoiceRepository : IInvoiceRepository
    {

        private readonly TaskDbContext _context;

        public InvoiceRepository(TaskDbContext context)
        {
            _context = context;
        }

        public async Task<Invoice> GetByIdAsync(int id)
        {
            return await _context.Invoices
                                 .Include(i => i.Order)
                                 .FirstOrDefaultAsync(i => i.InvoiceId == id);
        }

        public async Task<IEnumerable<Invoice>> GetAllAsync()
        {
            return await _context.Invoices
                                 .Include(i => i.Order)
                                 .ToListAsync();
        }
    }


}