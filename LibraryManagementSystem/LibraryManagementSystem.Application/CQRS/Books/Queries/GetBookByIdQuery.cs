using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs;
using MediatR;
namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Books.Queries
{
    public record GetBookByIdQuery(int Id) : IRequest<BookDto>;

}
