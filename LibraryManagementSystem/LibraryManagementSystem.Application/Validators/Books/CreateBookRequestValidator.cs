using LibraryManagementSystem.LibraryManagementSystem.Application.CQRS.Books.Commands;
using FluentValidation;
namespace LibraryManagementSystem.LibraryManagementSystem.Application.Validators.Books
{
    public class CreateBookCommandValidator : AbstractValidator<CreateBookCommand>
    {
        public CreateBookCommandValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Author is required")
                .MaximumLength(100).WithMessage("Author name cannot exceed 100 characters");

            RuleFor(x => x.ISBN)
                .NotEmpty().WithMessage("ISBN is required")
                .Length(10, 13).WithMessage("ISBN must be between 10 and 13 characters");

            RuleFor(x => x.CopiesAvailable)
                .GreaterThanOrEqualTo(0).WithMessage("Copies available cannot be negative");
        }
    }
}
