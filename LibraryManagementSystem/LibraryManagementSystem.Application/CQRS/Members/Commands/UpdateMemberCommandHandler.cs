using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.LibraryManagementSystem.Domain.Enums;
using AutoMapper;
using MediatR;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Members.Commands
{
    public class UpdateMemberCommandHandler : IRequestHandler<UpdateMemberCommand, MemberDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateMemberCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MemberDto> Handle(UpdateMemberCommand request, CancellationToken cancellationToken)
        {
            var member = await _unitOfWork.Members.GetByIdAsync(request.Id);
            if (member == null)
                throw new KeyNotFoundException($"Member with ID {request.Id} not found");

            if (!await _unitOfWork.Members.IsEmailUniqueAsync(request.Email, request.Id))
                throw new InvalidOperationException("Email already exists");

            if (!Enum.TryParse<Role>(request.Role, out var role))
                throw new ArgumentException("Invalid role");

            member.Name = request.Name;
            member.Email = request.Email;
            member.Role = role;

            _unitOfWork.Members.Update(member);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<MemberDto>(member);
        }
    }
}
