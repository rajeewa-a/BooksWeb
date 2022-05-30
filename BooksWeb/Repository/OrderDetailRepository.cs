using BooksWeb.Data;
using BooksWeb.Models;
using BooksWeb.Repository.IRepository;

namespace BooksWeb.Repository
{
    public class OrderDetailRepository : Repository<OrderDetail>, IOrderDetailRepository
    {
        private AppDbContext _db;

        public OrderDetailRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(OrderDetail obj)
        {
            _db.OrderDetails.Update(obj);
        }
    }
}
