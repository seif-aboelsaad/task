using Microsoft.AspNetCore.Identity;

namespace route_Tsak.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Address { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
