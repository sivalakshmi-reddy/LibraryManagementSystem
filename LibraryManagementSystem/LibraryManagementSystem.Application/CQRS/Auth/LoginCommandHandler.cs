using System.Security.Cryptography;
using System.Text;
using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs.Auth;
using LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces;
using MediatR;
namespace LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Auth
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, LoginResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtTokenService _jwtTokenService;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IJwtTokenService jwtTokenService)
        {
            _unitOfWork = unitOfWork;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                throw new ArgumentException("Email and password are required");
            }

            var member = await _unitOfWork.Members.GetByEmailAsync(request.Email);
            if (member == null)
                throw new UnauthorizedAccessException("Invalid email or password");

            if (!VerifyPassword(request.Password, member.PasswordHash))
                throw new UnauthorizedAccessException("Invalid email or password");

            var token = _jwtTokenService.GenerateToken(member);

            return new LoginResponse
            {
                Token = token,
                Role = member.Role.ToString(),
                UserId = member.Id
            };
        }

        private bool VerifyPassword(string password, string storedHash)
        {
            using var sha256 = SHA256.Create();
            var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var inputHash = Convert.ToBase64String(hashBytes);
            return inputHash == storedHash;
        }
    }
}
    
