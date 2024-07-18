using System.ComponentModel.DataAnnotations.Schema;

namespace route_Tsak.Models
{
    public class OrderItem
    {
        public int OrderItemId { get; set; }
        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Discount { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
