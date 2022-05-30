using BooksWeb.Models;

namespace BooksWeb.Repository.IRepository
{
    public interface IFeedbackRepository : IRepository<Feedback>
    {
        void Update(Feedback obj);
    }
}
