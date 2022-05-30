using BooksWeb.Data;
using BooksWeb.Models;
using BooksWeb.Repository.IRepository;

namespace BooksWeb.Repository
{
    public class CategoryRepository : Repository<Category>, ICategoryRepository
    {
        private AppDbContext _db;

        public CategoryRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Category obj)
        {
            _db.Categories.Update(obj);
        }
    }
}
