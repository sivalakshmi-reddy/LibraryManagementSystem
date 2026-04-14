using LibraryManagementSystem.LibraryManagementSystem.Domain.Enums;

namespace LibraryManagementSystem.LibraryManagementSystem.Domain.Entities
{
    public class Member
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public Role Role { get; set; }
        public DateTime JoinDate { get; set; }

        public  ICollection<BorrowingRecord> BorrowingRecords { get; set; } = new List<BorrowingRecord>();
    }
}
