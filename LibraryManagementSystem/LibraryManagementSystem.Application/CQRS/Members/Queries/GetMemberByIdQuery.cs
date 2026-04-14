using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs;
using MediatR;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Members.Queries
{
    public record GetMemberByIdQuery(int Id) : IRequest<MemberDto>;

}
