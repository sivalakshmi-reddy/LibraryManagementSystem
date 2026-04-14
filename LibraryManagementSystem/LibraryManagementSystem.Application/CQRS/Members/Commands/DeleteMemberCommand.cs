using MediatR;
namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Members.Commands
{
    public record DeleteMemberCommand(int Id) : IRequest<Unit>;

}
