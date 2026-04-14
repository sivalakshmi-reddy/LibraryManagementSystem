using LibraryManagementSystem.LibraryManagementSystem.Application.Interfaces;
using LibraryManagementSystem.LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.LibraryManagementSystem.Infrastructure.Data.Repositories
{
    public class MemberRepository : Repository<Member>, IMemberRepository
    {
        public MemberRepository(LibraryDbcontext context) : base(context)
        {
        }

        public async Task<Member?> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(m => m.Email == email);
        }

        public async Task<bool> IsEmailUniqueAsync(string email, int? excludeMemberId = null)
        {
            var query = _dbSet.Where(m => m.Email == email);

            if (excludeMemberId.HasValue)
            {
                query = query.Where(m => m.Id != excludeMemberId.Value);
            }

            return !await query.AnyAsync();
        }
    }
}
