using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs.Auth;
using MediatR;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Auth
{
    public record LoginCommand : IRequest<LoginResponse>
    {
        public string Email { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}
