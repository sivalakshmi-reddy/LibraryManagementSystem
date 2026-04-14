using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs;
using MediatR;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Borrowings.Commands
{
    public record ReturnBookCommand : IRequest<BorrowingDto>
    {
        public int BorrowingRecordId { get; set; }
    }
}
