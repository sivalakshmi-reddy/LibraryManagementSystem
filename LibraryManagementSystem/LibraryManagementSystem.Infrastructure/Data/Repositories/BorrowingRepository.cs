using LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.LibraryManagementSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.LibraryManagementSystem.Infrastructure.Data.Repositories
{
    public class BorrowingRepository : Repository<BorrowingRecord>, IBorrowingRepository
    {
        public BorrowingRepository(LibraryDbcontext context) : base(context)
        {
        }

        public async Task<IEnumerable<BorrowingRecord>> GetBorrowingsByMemberAsync(int memberId)
        {
            return await _dbSet
                .Include(br => br.Member)
                .Include(br => br.Book)
                .Where(br => br.MemberId == memberId)
                .OrderByDescending(br => br.BorrowDate)
                .ToListAsync();
        }

        public async Task<IEnumerable<BorrowingRecord>> GetOverdueBorrowingsAsync()
        {
            var currentDate = DateTime.UtcNow;

            return await _dbSet
                .Include(br => br.Member)
                .Include(br => br.Book)
                .Where(br => br.Status == BorrowingStatus.Borrowed && br.DueDate < currentDate)
                .ToListAsync();
        }

        public async Task<BorrowingRecord?> GetActiveBorrowingAsync(int memberId, int bookId)
        {
            return await _dbSet
                .FirstOrDefaultAsync(br => br.MemberId == memberId &&
                                         br.BookId == bookId &&
                                         br.Status == BorrowingStatus.Borrowed);
        }

        public async Task<int> GetActiveBorrowingsCountAsync(int memberId)
        {
            return await _dbSet
                .CountAsync(br => br.MemberId == memberId && br.Status == BorrowingStatus.Borrowed);
        }

        public override async Task<IEnumerable<BorrowingRecord>> GetAllAsync()
        {
            return await _dbSet
                .Include(br => br.Member)
                .Include(br => br.Book)
                .ToListAsync();
        }

        public override async Task<BorrowingRecord?> GetByIdAsync(int id)
        {
            return await _dbSet
                .Include(br => br.Member)
                .Include(br => br.Book)
                .FirstOrDefaultAsync(br => br.Id == id);
        }
    }
}
