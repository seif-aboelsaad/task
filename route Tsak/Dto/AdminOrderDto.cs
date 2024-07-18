using route_Tsak.Enum;

namespace route_Tsak.Dto
{
    public class AdminOrderDto
    {
        public int OrderId { get; set; }
        public string CustomerId { get; set; }
        public DateTime OrderDate { get; set; }
        public decimal TotalAmount { get; set; }
        public Statusenum statusenum { get; set; }
        public Paymentenum paymentenum { get; set; }

    }
}
