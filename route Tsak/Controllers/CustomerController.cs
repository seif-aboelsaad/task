using Microsoft.AspNetCore.Mvc;
using route_Tsak.Dto;
using route_Tsak.Models;
using route_Tsak.Services;

namespace route_Tsak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : Controller
    {
        public readonly IcostumerServices customerService;
        public CustomerController(IcostumerServices customerService)
        {
            this.customerService = customerService;
        }
        [HttpGet("GetAllCustomers")]
        public IActionResult GetAllCustomers()
        {
            var result = customerService.GetAllCustomers();
            return Ok(result);
        }

        [HttpGet("customer/{id}")]
        public IActionResult GetCustomerById(string id) { 
            var result = customerService.GetCustomerById(id);
            return Ok(result);
        }

        //[HttpPost("CreateCustomer")]
        //public async Task<IActionResult> CreateCustomer(Costumerdto customer)
        //{
        //    await customerService.AddCustomerAsync(customer);
        //    return Ok();
        //}

    }
}
