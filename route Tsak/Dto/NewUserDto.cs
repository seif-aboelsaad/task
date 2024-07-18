using System.ComponentModel.DataAnnotations;

namespace route_Tsak.Dto
{
    public class NewUserDto
    {
        [Required]
        public string Name { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [MinLength(6)]
        public string Password { get; set; }
        [Required]
        public string Address { get; set; }
    }
}
