using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces;
using MediatR;
using AutoMapper;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Borrowings.Queries
{
    public class GetBorrowingsByMemberQueryHandler : IRequestHandler<GetBorrowingsByMemberQuery, IEnumerable<BorrowingDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetBorrowingsByMemberQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BorrowingDto>> Handle(GetBorrowingsByMemberQuery request, CancellationToken cancellationToken)
        {
            var borrowings = await _unitOfWork.BorrowingRecords.GetBorrowingsByMemberAsync(request.MemberId);
            return _mapper.Map<IEnumerable<BorrowingDto>>(borrowings);
        }
    }
}
