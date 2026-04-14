using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LibraryManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class M1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Author = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    ISBN = table.Column<string>(type: "nvarchar(13)", maxLength: 13, nullable: false),
                    CopiesAvailable = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false),
                    JoinDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BorrowingRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MemberId = table.Column<int>(type: "int", nullable: false),
                    BookId = table.Column<int>(type: "int", nullable: false),
                    BorrowDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DueDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BorrowingRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BorrowingRecords_Books_BookId",
                        column: x => x.BookId,
                        principalTable: "Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BorrowingRecords_Members_MemberId",
                        column: x => x.MemberId,
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Books",
                columns: new[] { "Id", "Author", "CopiesAvailable", "ISBN", "Title" },
                values: new object[,]
                {
                    { 1, "F. Scott Fitzgerald", 3, "9780743273565", "The Great Gatsby" },
                    { 2, "Harper Lee", 2, "9780061120084", "To Kill a Mockingbird" },
                    { 3, "George Orwell", 4, "9780451524935", "1984" },
                    { 4, "Jane Austen", 3, "9780141439518", "Pride and Prejudice" }
                });

            migrationBuilder.InsertData(
                table: "Members",
                columns: new[] { "Id", "Email", "JoinDate", "Name", "PasswordHash", "Role" },
                values: new object[,]
                {
                    { 1, "librarian@library.com", new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "System Librarian", "JAvlGPq9JyTdtvBO6x2llnRI1+gxwIyPqCKAn3THIKk=", 2 },
                    { 2, "john.doe@email.com", new DateTime(2023, 12, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "John Doe", "VgA3boY9L1egU1GPMkrThAsLwjSLVzrygae3y+eiKMY=", 1 },
                    { 3, "jane.smith@email.com", new DateTime(2023, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jane Smith", "VgA3boY9L1egU1GPMkrThAsLwjSLVzrygae3y+eiKMY=", 1 },
                    { 4, "bob.johnson@email.com", new DateTime(2023, 12, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Bob Johnson", "VgA3boY9L1egU1GPMkrThAsLwjSLVzrygae3y+eiKMY=", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_ISBN",
                table: "Books",
                column: "ISBN",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BorrowingRecords_BookId",
                table: "BorrowingRecords",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_BorrowingRecords_MemberId",
                table: "BorrowingRecords",
                column: "MemberId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_Email",
                table: "Members",
                column: "Email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BorrowingRecords");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DropTable(
                name: "Members");
        }
    }
}
