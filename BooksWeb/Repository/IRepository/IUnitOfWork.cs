namespace BooksWeb.Repository.IRepository
{
    public interface IUnitOfWork
    {
        ICategoryRepository Category { get; }

        ICoverTypeRepository CoverType { get; }
        IBookRepository Book { get; }

        ICartRepository Cart { get; }
        IAppUserRepository AppUser { get; }
        IOrderDetailRepository OrderDetail { get; }
        IOrderHeaderRepository OrderHeader { get; }

        IFeedbackRepository Feedback { get; }

        void Save();
    }
}
