using LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Members.Commands;
using FluentValidation;
namespace LibraryManagementSystem.LibraryManagementSystem.Application.Validators.Members
{
    public class CreateMemberCommandValidator : AbstractValidator<CreateMemberCommand>
    {
        public CreateMemberCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters");

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");

            RuleFor(x => x.Role)
                .Must(role => role == "Member" || role == "Librarian")
                .WithMessage("Role must be either 'Member' or 'Librarian'");
        }
    }
}
