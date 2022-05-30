using System.ComponentModel.DataAnnotations;

namespace BooksWeb.Models
{
    public class Feedback
    {

        [Key]
        public int Id { get; set; }
        [Required]
        public string CustomerFeedback { get; set; }

        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
