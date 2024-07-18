using Microsoft.AspNetCore.Mvc;
using route_Tsak.Dto;
using route_Tsak.Models;
namespace route_Tsak.Repo
{
    public interface IOrderRepository
    {
        Task<string> AddAsync(Order order , List<OrderItemDto> orderitems);
        Order GetById(int id);
        List<Order> GetAll();
        Task UpdateAsync(AdminOrderDto order);
        Task DeleteAsync(int id);
        List<OrderItem> GetAllItems(int id);
    }
}
