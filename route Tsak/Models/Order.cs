using route_Tsak.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace route_Tsak.Models
{
    public class Order
    {
        public int OrderId { get; set; }
        [ForeignKey("Customer")]
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public Statusenum statusenum { get; set; }
        public Paymentenum paymentenum { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
        public ApplicationUser Customer { get; set; }
    }
}
