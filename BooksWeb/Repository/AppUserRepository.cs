using BooksWeb.Data;
using BooksWeb.Models;
using BooksWeb.Repository.IRepository;

namespace BooksWeb.Repository
{
    public class AppUserRepository : Repository<AppUser>, IAppUserRepository
    {
        private AppDbContext _db;

        public AppUserRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

    }
}
