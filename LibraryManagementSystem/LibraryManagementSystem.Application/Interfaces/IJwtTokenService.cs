using LibraryManagementSystem.LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces
{
    public interface IJwtTokenService
    {
        string GenerateToken(Member member);
    }
}
