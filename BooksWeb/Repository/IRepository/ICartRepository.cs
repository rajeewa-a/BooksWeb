using BooksWeb.Models;

namespace BooksWeb.Repository.IRepository
{
    public interface ICartRepository : IRepository<Cart>
    {
        int IncrementCount(Cart cart, int count);
        int DecrementCount(Cart cart, int count);
    }
}
