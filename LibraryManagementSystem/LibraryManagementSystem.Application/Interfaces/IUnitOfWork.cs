namespace LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IMemberRepository Members { get; }
        IBookRepository Books { get; }
        IBorrowingRepository BorrowingRecords { get; }
        Task<int> SaveChangesAsync();
    }
}
