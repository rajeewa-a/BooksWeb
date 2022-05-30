using BooksWeb.Models;

namespace BooksWeb.Repository.IRepository
{
    public interface ICategoryRepository : IRepository<Category>
    {
        void Update(Category obj);

    }
}
