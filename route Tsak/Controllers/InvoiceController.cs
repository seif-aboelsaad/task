using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using route_Tsak.Services;

namespace route_Tsak.Controllers
{
    public class InvoiceController : Controller
    {
        [Route("api/[controller]")]
        [ApiController]
        [Authorize(Roles = "Admin")]  // Ensure only admins can access these endpoints
        public class InvoicesController : ControllerBase
        {
            private readonly IInvoiceService _invoiceService;

            public InvoicesController(IInvoiceService invoiceService)
            {
                _invoiceService = invoiceService;
            }

            [HttpGet("{invoiceId}")]
            public async Task<IActionResult> GetInvoiceById(int invoiceId)
            {
                var invoice = await _invoiceService.GetByIdAsync(invoiceId);
                if (invoice == null)
                {
                    return NotFound();
                }
                return Ok(invoice);
            }

            [HttpGet]
            public async Task<IActionResult> GetAllInvoices()
            {
                var invoices = await _invoiceService.GetAllAsync();
                return Ok(invoices);
            }
        }
    }
}
