
using FluentValidation;
using LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Auth;
using LibraryManagementSystem.LibraryManagementSystem.Application.DTOs.Auth;

namespace LibraryManagementSystem.LibraryManagementSystem.Application.Validators.Auth
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email format");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters");
        }

        internal async Task ValidateAsync(LoginCommand command)
        {
            throw new NotImplementedException();
        }
    }
}
