using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs;
using LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.LibraryManagementSystem.Domain.Enums;
using AutoMapper;
using MediatR;
using System.Security.Cryptography;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Members.Commands
{
    public class CreateMemberCommandHandler : IRequestHandler<CreateMemberCommand, MemberDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateMemberCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<MemberDto> Handle(CreateMemberCommand request, CancellationToken cancellationToken)
        {
            if (!await _unitOfWork.Members.IsEmailUniqueAsync(request.Email))
                throw new InvalidOperationException("Email already exists");

            if (!Enum.TryParse<Role>(request.Role, out var role))
                throw new ArgumentException("Invalid role");

            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(request.Password));
            var passwordHash = Convert.ToBase64String(hashBytes);

            var member = new Member
            {
                Name = request.Name,
                Email = request.Email,
                PasswordHash = passwordHash,
                Role = role,
                JoinDate = DateTime.UtcNow
            };

            await _unitOfWork.Members.AddAsync(member);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<MemberDto>(member);
        }
    }
}
