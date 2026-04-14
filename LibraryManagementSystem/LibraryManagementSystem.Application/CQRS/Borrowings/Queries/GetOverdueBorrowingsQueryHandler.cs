using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces;
using MediatR;
using AutoMapper;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Borrowings.Queries
{
    public class GetOverdueBorrowingsQueryHandler : IRequestHandler<GetOverdueBorrowingsQuery, IEnumerable<BorrowingDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetOverdueBorrowingsQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BorrowingDto>> Handle(GetOverdueBorrowingsQuery request, CancellationToken cancellationToken)
        {
            var overdueBorrowings = await _unitOfWork.BorrowingRecords.GetOverdueBorrowingsAsync();
            return _mapper.Map<IEnumerable<BorrowingDto>>(overdueBorrowings);
        }
    }
}
