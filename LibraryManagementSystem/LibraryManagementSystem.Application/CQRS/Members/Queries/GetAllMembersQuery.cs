using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs;
using MediatR;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Members.Queries
{
    public record GetAllMembersQuery : IRequest<IEnumerable<MemberDto>>;

}
