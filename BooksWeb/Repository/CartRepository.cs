using BooksWeb.Data;
using BooksWeb.Models;
using BooksWeb.Repository.IRepository;

namespace BooksWeb.Repository
{
    public class CartRepository : Repository<Cart>, ICartRepository
    {
        private AppDbContext _db;

        public CartRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public int DecrementCount(Cart cart, int count)
        {
            cart.Count -= count;
            return cart.Count;
        }

        public int IncrementCount(Cart cart, int count)
        {
            cart.Count += count;
            return cart.Count;
        }
    }
}
