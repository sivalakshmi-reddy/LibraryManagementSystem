using LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces;
using MediatR;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Members.Commands
{
    public class DeleteMemberCommandHandler : IRequestHandler<DeleteMemberCommand, Unit>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteMemberCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Unit> Handle(DeleteMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(request.Id);
            if (member == null)
                throw new KeyNotFoundException($"Member with ID {request.Id} not found");

            var activeBorrowings = await _unitOfWork.BorrowingRecords.GetBorrowingsByMemberAsync(request.Id);
            if (activeBorrowings.Any(b => b.Status == Domain.Enums.BorrowingStatus.Borrowed))
                throw new InvalidOperationException("Cannot delete member with active borrowings");

            _unitOfWork.Members.Delete(member);
            await _unitOfWork.SaveChangesAsync();

            return Unit.Value;
        }
    }
}
