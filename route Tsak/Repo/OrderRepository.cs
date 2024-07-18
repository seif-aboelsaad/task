using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using route_Tsak.Dto;
using route_Tsak.Models;

namespace route_Tsak.Repo
{
    public class OrderRepository : IOrderRepository
    {
        public readonly TaskDbContext _context;
        public OrderRepository(TaskDbContext context)
        {
            _context = context;
        }
        public async Task<string> AddAsync(Order order , List<OrderItemDto> orderitems)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            foreach (var item in orderitems)
            {
                decimal ss = 0;
                if (_context.Products.FirstOrDefault(p => p.ProductId == item.ProductId) == null || _context.Products.FirstOrDefault(p => p.ProductId == item.ProductId).Stock <= item.Quantity)
                {
                    return "Product is out of stock";
                }
                else
                {
                    if (_context.Products.FirstOrDefault(p => p.ProductId == item.ProductId).Price >= (decimal)200)
                    {
                        ss = (decimal)10 / 100;
                    }
                    else if (_context.Products.FirstOrDefault(p => p.ProductId == item.ProductId).Price >= (decimal)100)
                    {
                        ss = (decimal)5 / 100;
                    }
                    var orderitem = new OrderItem()
                    {
                        OrderId = order.OrderId,
                        ProductId = item.ProductId,
                        Quantity = item.Quantity,
                        UnitPrice = _context.Products.FirstOrDefault(p => p.ProductId == item.ProductId).Price,
                        Discount = ss
                    };
                    _context.OrderItems.Add(orderitem);
                    _context.Products.FirstOrDefault(p => p.ProductId == item.ProductId).Stock-=item.Quantity;
                    await _context.SaveChangesAsync();
                }

            }
            await _context.SaveChangesAsync();
            return "Product added successfully";
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public List<Order> GetAll()
        {
            return _context.Orders.ToList();
        }

        public List<OrderItem> GetAllItems(int id)
        {
            return _context.OrderItems.Where(c => c.OrderId == id).ToList();
        }

        public Order GetById(int id)
        {
            return _context.Orders.FirstOrDefault(o => o.OrderId == id);
        }

        public async Task UpdateAsync(AdminOrderDto order)
        {
            var oldorder = _context.Orders.FirstOrDefault(c=>c.OrderId == order.OrderId);
            oldorder.TotalAmount = order.TotalAmount;
            oldorder.statusenum = order.statusenum;
            oldorder.paymentenum = order.paymentenum;
            await _context.SaveChangesAsync();
        }

    }
}
