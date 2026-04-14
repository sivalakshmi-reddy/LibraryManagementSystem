using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs;
using MediatR;
namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Members.Commands
{
    public record CreateMemberCommand : IRequest<MemberDto>
    {
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "Member";
    }
}