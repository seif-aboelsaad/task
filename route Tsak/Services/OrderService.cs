using Microsoft.AspNetCore.Mvc;
using route_Tsak.Dto;
using route_Tsak.Enum;
using route_Tsak.Models;
using route_Tsak.Repo;
using System.ComponentModel;

namespace route_Tsak.Services
{
    public class OrderService : IOrderService
    {
        public readonly IOrderRepository _orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<string> AddOrderAsync(UserOrderDto orderdto)
        {
            var Order = new Order();
            Order.CustomerId = orderdto.Id;
            Order.OrderDate = DateTime.Now;
            Order.TotalAmount = orderdto.TotalAmount;
            Order.paymentenum = orderdto.paymentenum;
            Order.statusenum = (Statusenum)1;
            string result = await _orderRepository.AddAsync(Order , orderdto.OrderItems);
            return result;

        }

        public List<AdminOrderDto> GetAllOrders()
        {
            List<Order> orders = _orderRepository.GetAll();
            List<AdminOrderDto> adminOrderDtos = new List<AdminOrderDto>();
            foreach (var order in orders)
            {
                var orderDto = new AdminOrderDto()
                {
                    OrderId = order.OrderId,
                    CustomerId = order.CustomerId,
                    OrderDate = order.OrderDate,
                    TotalAmount = order.TotalAmount,
                    statusenum = order.statusenum,
                    paymentenum = order.paymentenum,
                };
                adminOrderDtos.Add(orderDto);
            }
            return adminOrderDtos;
        }
        
        public UserOrderDto GetOrderbyId(int id)
        {
            var order = new UserOrderDto();
            var wantedorder = _orderRepository.GetById(id); // apply jwt
            order.OrderDate = wantedorder.OrderDate;
            order.TotalAmount = wantedorder.TotalAmount;
            order.paymentenum = wantedorder.paymentenum;
            return order;
        }

        public async Task UpdateOrderAsync(AdminOrderDto orderdto)
        {
            await _orderRepository.UpdateAsync(orderdto); // apply jwt
        }
    }
}
