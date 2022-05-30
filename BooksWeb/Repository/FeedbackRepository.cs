using BooksWeb.Data;
using BooksWeb.Models;
using BooksWeb.Repository.IRepository;

namespace BooksWeb.Repository
{
    public class FeedbackRepository : Repository<Feedback>, IFeedbackRepository
    {
        private AppDbContext _db;

        public FeedbackRepository(AppDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Feedback obj)
        {
            throw new NotImplementedException();
        }
    }
}
