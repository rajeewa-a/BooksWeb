using System.ComponentModel.DataAnnotations;

namespace BooksWeb.Models
{
    public class CoverType
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Cover Type")]
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
    }
}
