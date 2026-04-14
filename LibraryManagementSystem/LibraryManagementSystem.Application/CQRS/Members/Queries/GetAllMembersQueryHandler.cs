using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces;
using MediatR;
using AutoMapper;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Members.Queries
{
    public class GetAllMembersQueryHandler : IRequestHandler<GetAllMembersQuery, IEnumerable<MemberDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetAllMembersQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MemberDto>> Handle(GetAllMembersQuery request, CancellationToken cancellationToken)
        {
            var members = await _unitOfWork.Members.GetAllAsync();
            return _mapper.Map<IEnumerable<MemberDto>>(members);
        }
    }
}
    
