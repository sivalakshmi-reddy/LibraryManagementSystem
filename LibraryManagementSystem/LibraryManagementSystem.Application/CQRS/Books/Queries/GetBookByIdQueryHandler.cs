using LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Books.Commands;
using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces;
using MediatR;
using AutoMapper;
namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Books.Queries
{
    public class GetBookByIdQueryHandler : IRequestHandler<GetBookByIdQuery, BookDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBookByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BookDto> Handle(GetBookByIdQuery request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(request.Id);
            if (book == null)
                throw new KeyNotFoundException($"Book with ID {request.Id} not found");

            return _mapper.Map<BookDto>(book);
        }
    }
}
