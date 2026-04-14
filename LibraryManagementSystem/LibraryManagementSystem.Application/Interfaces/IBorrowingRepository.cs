using LibraryManagementSystem.LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces
{
    public interface IBorrowingRepository : IRepository<BorrowingRecord>
    {
        Task<IEnumerable<BorrowingRecord>> GetBorrowingsByMemberAsync(int memberId);
        Task<IEnumerable<BorrowingRecord>> GetOverdueBorrowingsAsync();
        Task<BorrowingRecord?> GetActiveBorrowingAsync(int memberId, int bookId);
        Task<int> GetActiveBorrowingsCountAsync(int memberId);
    }
}
    