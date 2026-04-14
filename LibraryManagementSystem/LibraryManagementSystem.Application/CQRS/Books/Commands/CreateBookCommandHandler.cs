using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.LibraryManagementSystem.Domain.Entities;
using MediatR;
using AutoMapper;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Books.Commands
{
    public class CreateBookCommandHandler : IRequestHandler<CreateBookCommand, BookDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateBookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BookDto> Handle(CreateBookCommand request, CancellationToken cancellationToken)
        {
            var existingBook = await _unitOfWork.Books.GetByISBNAsync(request.ISBN);
            if (existingBook != null)
                throw new InvalidOperationException("Book with this ISBN already exists");

            var book = new Book
            {
                Title = request.Title,
                Author = request.Author,
                ISBN = request.ISBN,
                CopiesAvailable = request.CopiesAvailable
            };

            await _unitOfWork.Books.AddAsync(book);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BookDto>(book);
        }
    }
}
