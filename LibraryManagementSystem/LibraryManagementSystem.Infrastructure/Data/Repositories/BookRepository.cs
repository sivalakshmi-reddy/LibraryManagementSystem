using LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.LibraryManagementSystem.Infrastructure.Data.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(LibraryDbcontext context) : base(context)
        {
        }

        public async Task<Book?> GetByISBNAsync(string isbn)
        {
            return await _dbSet.FirstOrDefaultAsync(b => b.ISBN == isbn);
        }

        public async Task<IEnumerable<Book>> GetAvailableBooksAsync()
        {
            return await _dbSet.Where(b => b.CopiesAvailable > 0).ToListAsync();
        }
    }
}
