using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace DnTech_ECommerce.Models
{
    public class User : IdentityUser
    {
        public string? FullName { get; set; }

        public string? Address { get; set; }

        [StringLength(50)]
        public string? City { get; set; }

        [StringLength(10)]
        public string? ZipCode { get; set; }

        [StringLength(50)]
        public string? Country { get; set; }

        public bool IsActive { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
}
