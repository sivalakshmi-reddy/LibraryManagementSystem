using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs;
using MediatR;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Borrowings.Queries
{
    public record GetBorrowingsByMemberQuery(int MemberId) : IRequest<IEnumerable<BorrowingDto>>;

}
