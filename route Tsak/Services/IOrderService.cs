using Microsoft.AspNetCore.Mvc;
using route_Tsak.Dto;

namespace route_Tsak.Services
{
    public interface IOrderService
    {
        Task<string> AddOrderAsync(UserOrderDto orderdto);
        UserOrderDto GetOrderbyId(int id);
        List<AdminOrderDto> GetAllOrders();
        Task UpdateOrderAsync(AdminOrderDto orderdto);
    }
}
