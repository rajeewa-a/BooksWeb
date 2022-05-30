using BooksWeb.Data;
using BooksWeb.Models;
using BooksWeb.Repository.IRepository;


namespace BooksWeb.Repository
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        private AppDbContext _db;

        public BookRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }


        public void Update(Book obj)
        {
            var objFromDb = _db.Books.FirstOrDefault(u => u.Id == obj.Id);
            if (objFromDb != null)
            {
                objFromDb.Title = obj.Title;
                objFromDb.ISBN = obj.ISBN;
                objFromDb.Price = obj.Price;
                objFromDb.Description = obj.Description;
                objFromDb.CategoryId = obj.CategoryId;
                objFromDb.Author = obj.Author;
                objFromDb.CoverTypeId = obj.CoverTypeId;
                if (obj.ImageUrl != null)
                {
                    objFromDb.ImageUrl = obj.ImageUrl;
                }
            }
        }
    }
}
