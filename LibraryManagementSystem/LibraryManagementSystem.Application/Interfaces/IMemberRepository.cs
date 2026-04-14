using LibraryManagementSystem.LibraryManagementSystem.Domain.Entities;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces
{
    public interface IMemberRepository : IRepository<Member>
    {
        Task<Member?> GetByEmailAsync(string email);
        Task<bool> IsEmailUniqueAsync(string email, int? excludeMemberId = null);
    }
}
