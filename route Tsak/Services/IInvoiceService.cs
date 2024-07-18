using route_Tsak.Dto;

namespace route_Tsak.Services
{
    public interface IInvoiceService
    {
        Task<InvoiceDto> GetByIdAsync(int id);
        Task<IEnumerable<InvoiceDto>> GetAllAsync();
    }
}
