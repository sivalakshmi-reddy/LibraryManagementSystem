using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.LibraryManagementSystem.Domain.Enums;
using MediatR;
using AutoMapper;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Borrowings.Commands
{
    public class ReturnBookCommandHandler : IRequestHandler<ReturnBookCommand, BorrowingDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ReturnBookCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BorrowingDto> Handle(ReturnBookCommand request, CancellationToken cancellationToken)
        {
            var borrowingRecord = await _unitOfWork.BorrowingRecords.GetByIdAsync(request.BorrowingRecordId);
            if (borrowingRecord == null)
                throw new KeyNotFoundException($"Borrowing record with ID {request.BorrowingRecordId} not found");

            if (borrowingRecord.Status == BorrowingStatus.Returned)
                throw new InvalidOperationException("Book has already been returned");

            var book = await _unitOfWork.Books.GetByIdAsync(borrowingRecord.BookId);
            if (book == null)
                throw new KeyNotFoundException("Associated book not found");

            borrowingRecord.ReturnDate = DateTime.UtcNow;
            borrowingRecord.Status = BorrowingStatus.Returned;

            book.CopiesAvailable++;

            _unitOfWork.BorrowingRecords.Update(borrowingRecord);
            _unitOfWork.Books.Update(book);
            await _unitOfWork.SaveChangesAsync();

            var borrowingDto = _mapper.Map<BorrowingDto>(borrowingRecord);
            borrowingDto.MemberName = borrowingRecord.Member.Name;
            borrowingDto.BookTitle = book.Title;

            return borrowingDto;
        }
    }
}
