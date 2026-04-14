using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs;
using MediatR;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Borrowings.Commands
{
    public record BorrowBookCommand : IRequest<BorrowingDto>
    {
        public int MemberId { get; set; }
        public int BookId { get; set; }
    }
}
