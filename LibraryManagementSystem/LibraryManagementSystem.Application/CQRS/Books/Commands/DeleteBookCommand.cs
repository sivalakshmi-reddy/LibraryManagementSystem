using MediatR;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Books.Commands
{
    public record DeleteBookCommand(int Id) : IRequest<Unit>;

}
