📚 Library Management System
A robust and scalable Library Management System API built using ASP.NET Core Web API following Clean Architecture and CQRS pattern.

🚀 Tech Stack
Backend: ASP.NET Core Web API (.NET 8)

Architecture: Clean Architecture (Layered)

Pattern: CQRS (Command Query Responsibility Segregation)

ORM: Entity Framework Core

Database: SQL Server

Authentication: JWT (JSON Web Tokens)

Validation: FluentValidation

Mapping: AutoMapper

Mediator Pattern: MediatR

📂 Project Structure
LibraryManagementSystem/
│
├── LibraryManagement.WebAPI        # API Layer (Controllers)
├── LibraryManagementSystem.Application   # Business Logic (CQRS, DTOs, Interfaces)
├── LibraryManagementSystem.Domain        # Entities & Enums
├── LibraryManagementSystem.Infrastructure # Data Access, Repositories
├── Migrations                      # Database Migrations
✨ Features
🔐 User Authentication (JWT-based login)

📚 Book Management (Add, Update, Delete, Get)

👤 Member Management

🔄 Borrow & Return Books

⏰ Track Overdue Books

🧠 Clean separation of concerns using CQRS

🧪 Input validation using FluentValidation

⚙️ Getting Started
🔹 Prerequisites
.NET 8 SDK

SQL Server

Visual Studio / VS Code

🔹 Setup Instructions
Clone the repository:

git clone https://github.com/your-username/LibraryManagementSystem.git
Navigate to project:

cd LibraryManagementSystem
Update connection string in:

appsettings.json
Apply migrations:

dotnet ef database update
Run the application:

dotnet run
📌 API Endpoints (Sample)
Method	Endpoint	Description
POST	/api/auth/login	User Login
GET	/api/books	Get all books
POST	/api/books	Create book
PUT	/api/books/{id}	Update book
DELETE	/api/books/{id}	Delete book
🧠 Architecture Highlights
✔ Clean Architecture (Separation of Concerns)

✔ CQRS Pattern for scalability

✔ Repository + Unit of Work Pattern

✔ Dependency Injection

✔ SOLID Principles

📸 Future Enhancements
🔹 Role-based Authorization

🔹 Swagger Documentation Improvements

🔹 Unit Testing with xUnit

🔹 Docker Support

👩‍💻 Author

Sivalakshmi Reddy
