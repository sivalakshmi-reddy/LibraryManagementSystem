using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.LibraryManagementSystem.Domain.Enums;
using MediatR;
using AutoMapper;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Borrowings.Commands
{
    public class BorrowBookCommandHandler : IRequestHandler<BorrowBookCommand, BorrowingDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BorrowBookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BorrowingDto> Handle(BorrowBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(request.BookId);
            if (book == null)
                throw new KeyNotFoundException($"Book with ID {request.BookId} not found");

            if (book.CopiesAvailable == 0)
                throw new InvalidOperationException("Book is not available for borrowing");

            var member = await _unitOfWork.Members.GetByIdAsync(request.MemberId);
            if (member == null)
                throw new KeyNotFoundException($"Member with ID {request.MemberId} not found");

            var existingBorrowing = await _unitOfWork.BorrowingRecords.GetActiveBorrowingAsync(request.MemberId, request.BookId);
            if (existingBorrowing != null)
                throw new InvalidOperationException("Member has already borrowed this book and not returned it");

            var activeBorrowingsCount = await _unitOfWork.BorrowingRecords.GetActiveBorrowingsCountAsync(request.MemberId);
            if (activeBorrowingsCount >= 3)
                throw new InvalidOperationException("Member cannot borrow more than 3 books");

            var borrowingRecord = new BorrowingRecord
            {
                MemberId = request.MemberId,
                BookId = request.BookId,
                BorrowDate = DateTime.UtcNow,
                DueDate = DateTime.UtcNow.AddDays(14), 
                ReturnDate = null,
                Status = BorrowingStatus.Borrowed
            };

            book.CopiesAvailable--;

            await _unitOfWork.BorrowingRecords.AddAsync(borrowingRecord);
            _unitOfWork.Books.Update(book);
            await _unitOfWork.SaveChangesAsync();

            var borrowingDto = _mapper.Map<BorrowingDto>(borrowingRecord);
            borrowingDto.MemberName = member.Name;
            borrowingDto.BookTitle = book.Title;

            return borrowingDto;
        }
    }
}
