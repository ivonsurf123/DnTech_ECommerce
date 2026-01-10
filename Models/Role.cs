using Microsoft.AspNetCore.Identity;

namespace DnTech_ECommerce.Models
{
    public class Role : IdentityRole
    {
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
