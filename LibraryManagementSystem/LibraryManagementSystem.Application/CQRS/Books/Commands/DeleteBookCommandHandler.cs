using LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces;
using MediatR;
namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Books.Commands
{
    public class DeleteBookCommandHandler : IRequestHandler<DeleteBookCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteBookCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteBookCommand request, CancellationToken cancellationToken)
        {
            var book = await _unitOfWork.Books.GetByIdAsync(request.Id);
            if (book == null)
                throw new KeyNotFoundException($"Book with ID {request.Id} not found");

            var borrowings = book.BorrowingRecords.Where(b => b.Status == Domain.Enums.BorrowingStatus.Borrowed);
            if (borrowings.Any())
                throw new InvalidOperationException("Cannot delete book with active borrowings");

            _unitOfWork.Books.Delete(book);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
