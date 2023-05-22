using FluentValidation;

namespace Avans.Demo.Domain.Validators
{
    /// <summary>
    /// Validator for type ApiBook
    /// </summary>
    public class ApiBookValidator : AbstractValidator<ApiBook>
    {
        public ApiBookValidator()
        {
            RuleFor(x => x.ISBN).NotEmpty().WithMessage("ISBN is required");
            RuleFor(x => x.Author).NotEmpty().WithMessage("Author is required");
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
        }
    }
}
