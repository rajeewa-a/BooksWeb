using BooksWeb.Data;
using BooksWeb.Repository.IRepository;

namespace BooksWeb.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            CoverType = new CoverTypeRepository(_db);
            Book = new BookRepository(_db);
            AppUser = new AppUserRepository(_db);
            Cart = new CartRepository(_db);
            OrderHeader = new OrderHeaderRepository(_db);
            OrderDetail = new OrderDetailRepository(_db);
            Feedback = new FeedbackRepository(_db);
           
        }
        public ICategoryRepository Category { get; private set; }
        public ICoverTypeRepository CoverType { get; private set; }
        public IBookRepository Book { get; private set; }


        public ICartRepository Cart { get; private set; }

        public IAppUserRepository AppUser { get; private set; }
        public IOrderHeaderRepository OrderHeader { get; private set; }
        public IOrderDetailRepository OrderDetail { get; private set; }

        public IFeedbackRepository Feedback { get; private set; }

        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
