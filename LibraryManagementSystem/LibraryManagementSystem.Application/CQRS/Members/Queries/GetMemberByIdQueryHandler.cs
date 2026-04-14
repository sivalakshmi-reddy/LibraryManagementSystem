using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces;
using MediatR;
using AutoMapper;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Members.Queries
{
    public class GetMemberByIdQueryHandler : IRequestHandler<GetMemberByIdQuery, MemberDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetMemberByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MemberDto> Handle(GetMemberByIdQuery request, CancellationToken cancellationToken)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(request.Id);
            if (member == null)
                throw new KeyNotFoundException($"Member with ID {request.Id} not found");

            return _mapper.Map<MemberDto>(member);
        }
    }
    
}
