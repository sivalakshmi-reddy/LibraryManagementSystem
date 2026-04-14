using LibraryManagementSystem.LibraryManagementSystem.Domain.Entities;
using LibraryManagementSystem.LibraryManagementSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace LibraryManagementSystem.LibraryManagementSystem.Infrastructure.Data
{
    public class SeedData
    {
       
            public static void Seed(ModelBuilder modelBuilder)
            {
                var members = GetMembers();
                modelBuilder.Entity<Member>().HasData(members);

                var books = GetBooks();
                modelBuilder.Entity<Book>().HasData(books);

            }

            private static List<Member> GetMembers()
            {
                var members = new List<Member>();
                var joinDate = new DateTime(2024, 1, 1);
                var librarianPassword = HashPassword("admin123");
                members.Add(new Member
                {
                    Id = 1,
                    Name = "System Librarian",
                    Email = "librarian@library.com",
                    PasswordHash = librarianPassword,
                    Role = Role.Librarian,
                    JoinDate = joinDate
                });

                var memberPassword = HashPassword("member123");
                members.Add(new Member
                {
                    Id = 2,
                    Name = "John Doe",
                    Email = "john.doe@email.com",
                    PasswordHash = memberPassword,
                    Role = Role.Member,
                    JoinDate = joinDate.AddDays(-30)
                });

                members.Add(new Member
                {
                    Id = 3,
                    Name = "Jane Smith",
                    Email = "jane.smith@email.com",
                    PasswordHash = memberPassword,
                    Role = Role.Member,
                    JoinDate = joinDate.AddDays(-15)
                });

                members.Add(new Member
                {
                    Id = 4,
                    Name = "Bob Johnson",
                    Email = "bob.johnson@email.com",
                    PasswordHash = memberPassword,
                    Role = Role.Member,
                    JoinDate = joinDate.AddDays(-7)
                });

                return members;
            }

            private static List<Book> GetBooks()
            {
                return new List<Book>
            {
                new Book { Id = 1, Title = "The Great Gatsby", Author = "F. Scott Fitzgerald", ISBN = "9780743273565", CopiesAvailable = 3 },
                new Book { Id = 2, Title = "To Kill a Mockingbird", Author = "Harper Lee", ISBN = "9780061120084", CopiesAvailable = 2 },
                new Book { Id = 3, Title = "1984", Author = "George Orwell", ISBN = "9780451524935", CopiesAvailable = 4 },
                new Book { Id = 4, Title = "Pride and Prejudice", Author = "Jane Austen", ISBN = "9780141439518", CopiesAvailable = 3 },
               
                };
            }

            private static string HashPassword(string password)
            {
                using var sha256 = SHA256.Create();
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
    }

}
