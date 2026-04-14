using LibraryManagementSystem.LibraryManagementSystem.Domain.Enums;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.DTOs
{
    public class BorrowingDto
    {
        public int Id { get; set; }
        public int MemberId { get; set; }
        public string MemberName { get; set; } = string.Empty;
        public int BookId { get; set; }
        public string BookTitle { get; set; } = string.Empty;
        public DateTime BorrowDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }
        public BorrowingStatus Status { get; set; }
    }
}
