using BooksWeb.Data;
using BooksWeb.Models;
using BooksWeb.Repository.IRepository;

namespace BooksWeb.Repository
{
    public class CoverTypeRepository : Repository<CoverType>, ICoverTypeRepository
    {
        private AppDbContext _db;

        public CoverTypeRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(CoverType obj)
        {
            _db.CoverTypes.Update(obj);
        }
    }
}
