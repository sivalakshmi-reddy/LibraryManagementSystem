using LibraryManagementSystem.LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces
{
    public interface IBookRepository : IRepository<Book>
    {
        Task<Book?> GetByISBNAsync(string isbn);
        Task<IEnumerable<Book>> GetAvailableBooksAsync();
    }
}
