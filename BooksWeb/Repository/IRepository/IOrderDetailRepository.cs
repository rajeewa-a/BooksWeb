using BooksWeb.Models;

namespace BooksWeb.Repository.IRepository
{
    public interface IOrderDetailRepository : IRepository<OrderDetail>
    {
        void Update(OrderDetail obj);
    }
}
