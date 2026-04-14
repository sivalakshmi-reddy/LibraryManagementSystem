using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs;
using MediatR;
namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Books.Commands
{
    public record UpdateBookCommand : IRequest<BookDto>
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        public string ISBN { get; set; } = string.Empty;
        public int CopiesAvailable { get; set; }
    }
}
