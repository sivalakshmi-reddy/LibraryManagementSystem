using LibraryManagementSystem.LibraryManagementSystem.Domain.Enums;

namespace LibraryManagementSystem.LibraryManagementSystem.Domain.Entities
{
    public class BorrowingRecord
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public int BookId { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public BorrowingStatus Status { get; set; }
        public  Member Member { get; set; } = null!;
        public Book Book { get; set; } = null!;
    }
}
