using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces;
using AutoMapper;
using MediatR;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Books.Commands
{
    public class UpdateBookCommandHandler : IRequestHandler<UpdateBookCommand, BookDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateBookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BookDto> Handle(UpdateBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(request.Id);
            if (book == null)
                throw new KeyNotFoundException($"Book with ID {request.Id} not found");

            var existingBook = await _unitOfWork.Books.GetByISBNAsync(request.ISBN);
            if (existingBook != null && existingBook.Id != request.Id)
                throw new InvalidOperationException("Book with this ISBN already exists");

            book.Title = request.Title;
            book.Author = request.Author;
            book.ISBN = request.ISBN;
            book.CopiesAvailable = request.CopiesAvailable;

            _unitOfWork.Books.Update(book);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<BookDto>(book);
        }
    }
}
