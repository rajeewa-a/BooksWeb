using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace BooksWeb.Models
{
    public class AppUser : IdentityUser
  
        {
        [Required]
        public string Name { get; set; }
        public string? Address { get; set; }
        public string? PostalCode { get; set; }

        public string? Nic { get; set; }
}
}
