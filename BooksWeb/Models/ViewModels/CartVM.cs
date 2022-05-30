namespace BooksWeb.Models.ViewModels
{
    public class CartVM
    {
        public IEnumerable<Cart> ListCart { get; set; }

       public OrderHeader OrderHeader { get; set; }
    }
}
