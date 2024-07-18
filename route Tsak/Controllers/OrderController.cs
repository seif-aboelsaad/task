using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using route_Tsak.Dto;
using route_Tsak.Services;
using System.Security.Claims;

namespace route_Tsak.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        public readonly IOrderService orderservice;
        public OrderController(IOrderService _orderservice) { 
            orderservice = _orderservice;
        }
        [HttpPost("CreateOrder")]
        [Authorize]
        public async Task<IActionResult> NewOrder(UserOrderDto userorder)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            userorder.Id = userId;
            string result = await orderservice.AddOrderAsync(userorder);
            if(result == "Product added successfully")
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }

        }
        [HttpGet("GetOrder")]
        public IActionResult GetOrder(int id)
        { 
            return Ok(orderservice.GetOrderbyId(id));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("GetAllOrders")]
        public IActionResult GetAllOrders()
        {
            var result = orderservice.GetAllOrders();
            return Ok(result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("UpdateOrder")]
        public async Task<IActionResult> EditOrder(AdminOrderDto editedorder)
        {
            await orderservice.UpdateOrderAsync(editedorder);
            return Ok();
        }
    }
}
