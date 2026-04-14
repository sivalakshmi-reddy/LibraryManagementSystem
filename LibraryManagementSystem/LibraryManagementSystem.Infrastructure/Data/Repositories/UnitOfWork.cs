using LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces;

namespace LibraryManagementSystem.LibraryManagementSystem.Infrastructure.Data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly LibraryDbcontext _context;
        private IMemberRepository _members;
        private IBookRepository _books;
        private IBorrowingRepository _borrowingRecords;

        public UnitOfWork(LibraryDbcontext context)
        {
            _context = context;
        }

        public IMemberRepository Members => _members ??= new MemberRepository(_context);
        public IBookRepository Books => _books ??= new BookRepository(_context);
        public IBorrowingRepository BorrowingRecords => _borrowingRecords ??= new BorrowingRepository(_context);

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }



        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
