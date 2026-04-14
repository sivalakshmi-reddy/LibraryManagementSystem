using LibraryManagementSystem.LibraryManagementSystem.Domain.Enums;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.DTOs
{
    public class MemberDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public Role Role { get; set; }
        public DateTime JoinDate { get; set; }
    }
}
