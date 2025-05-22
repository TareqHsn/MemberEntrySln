using FluentValidation;
using MemberEntry.Models;

public class MemeberBasicInfoModelValidator : AbstractValidator<MemberBasicInfoModel>
{
    public MemeberBasicInfoModelValidator()
    {
        RuleFor(p => p.NameInEnglish)
            .NotEmpty().WithMessage("Please enter 'English Name'.")
            .MinimumLength(3).WithMessage("Minimum length of 'English Name' is 3 characters.")
            .MaximumLength(150).WithMessage("Maximum length of 'English Name' is 150 characters.");

        RuleFor(p => p.NameInBangla)
            .NotEmpty().WithMessage("Please enter 'Bangla Name'.")
            .MinimumLength(3).WithMessage("Minimum length of 'Bangla Name' is 3 characters.")
            .MaximumLength(150).WithMessage("Maximum length of 'Bangla Name' is 150 characters.");

    }
}