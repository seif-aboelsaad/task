using route_Tsak.Dto;
using route_Tsak.Repo;

namespace route_Tsak.Services
{
    public class InvoiceServices : IInvoiceService
    {
        public readonly InvoiceRepository _invoiceRepository;
        public InvoiceServices(InvoiceRepository invoiceRepository1)
        {
              _invoiceRepository= invoiceRepository1;
            
        }
        async Task<IEnumerable<InvoiceDto>> IInvoiceService.GetAllAsync()
        {
            var invoices = await _invoiceRepository.GetAllAsync();

            return invoices.Select(invoice => new InvoiceDto
            {
                InvoiceId = invoice.InvoiceId,
                OrderId = invoice.OrderId,
                InvoiceDate = invoice.InvoiceDate,
                TotalAmount = invoice.TotalAmount
            }).ToList();
        }

        async Task<InvoiceDto> IInvoiceService.GetByIdAsync(int id)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(id);
            if (invoice == null)
            {
                return null;
            }

            return new InvoiceDto
            {
                InvoiceId = invoice.InvoiceId,
                OrderId = invoice.OrderId,
                InvoiceDate = invoice.InvoiceDate,
                TotalAmount = invoice.TotalAmount
            };
        }
    }
}
