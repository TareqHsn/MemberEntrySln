using FluentValidation;
using MemberEntry.Models;

namespace MemberEntry.Validator
{
    public class PassportTypeModelValidator : AbstractValidator<PassportType>
    {
        public PassportTypeModelValidator()
        {
            RuleFor(p => p.Name)
                .NotEmpty().WithMessage("Please enter 'Name'.")
                .MinimumLength(3).WithMessage("Minimum length of 'Name' is 3 characters.")
                .MaximumLength(150).WithMessage("Maximum length of 'Name' is 100 characters.");
        }
    }
}
