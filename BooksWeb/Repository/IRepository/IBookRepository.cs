using BooksWeb.Models;

namespace BooksWeb.Repository.IRepository
{
    public interface IBookRepository : IRepository<Book>
    {
        void Update(Book obj);
    }
}
