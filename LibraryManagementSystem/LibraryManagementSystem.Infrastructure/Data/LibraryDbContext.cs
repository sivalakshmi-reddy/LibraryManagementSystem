using LibraryManagementSystem.LibraryManagementSystem.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.LibraryManagementSystem.Infrastructure.Data
{
    public class LibraryDbcontext :DbContext
    {
        public LibraryDbcontext(DbContextOptions<LibraryDbcontext> options) : base(options)
        {
        }

        public DbSet<Member> Members { get; set; }
        public DbSet<Book> Books { get; set; }
        public DbSet<BorrowingRecord> BorrowingRecords { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            SeedData.Seed(modelBuilder);
        }
    }
}
