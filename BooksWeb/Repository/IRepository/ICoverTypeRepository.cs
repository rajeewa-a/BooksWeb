using BooksWeb.Models;

namespace BooksWeb.Repository.IRepository
{

    public interface ICoverTypeRepository : IRepository<CoverType>
    {
        void Update(CoverType obj);
    }
}
