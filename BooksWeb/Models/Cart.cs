using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BooksWeb.Models
{
    public class Cart
    {
        public int Id { get; set; }

        public int BookId { get; set; }
        [ForeignKey("BookId")]
        [ValidateNever]
        public Book Book { get; set; }
        [Range(1, 1000, ErrorMessage = "Please enter a value between 1 and 1000")]
        public int Count { get; set; }

        public string AppUserId { get; set; }
        [ForeignKey("AppUserId")]
        [ValidateNever]
        public AppUser AppUser { get; set; }

        [NotMapped]
        public double Price { get; set; }
    }
}
