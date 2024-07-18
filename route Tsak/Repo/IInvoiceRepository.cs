using route_Tsak.Models;

namespace route_Tsak.Repo
{
    public interface IInvoiceRepository
    {
        Task<Invoice> GetByIdAsync(int id);
        Task<IEnumerable<Invoice>> GetAllAsync();
    }
}
