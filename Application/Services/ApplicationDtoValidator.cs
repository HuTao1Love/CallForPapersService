using Application.Contracts;
using FluentValidation;
using Models;

namespace Application.Services;

public class ApplicationDtoValidator : AbstractValidator<ApplicationDto>
{
    public ApplicationDtoValidator()
    {
        RuleSet(
            "OnSubmit",
            () =>
        {
            RuleFor(a => a.Name).NotEmpty();
            RuleFor(a => a.Activity).NotEmpty();
            RuleFor(a => a.Outline).NotEmpty();
        });

        RuleFor(a => a.Author).NotEmpty();

        RuleFor(a => a.Activity).IsEnumName(typeof(Activity), false);
        RuleFor(a => a.Name).MaximumLength(100);
        RuleFor(a => a.Description).MaximumLength(300);
        RuleFor(a => a.Outline).MaximumLength(1000);

        RuleFor(a => a)
            .Must(HaveAtLeastOneFieldFilled)
            .WithMessage("At least one of Activity, Name, Description, or Outline must be filled.");
    }

    private static bool HaveAtLeastOneFieldFilled(ApplicationDto dto)
    {
        return !string.IsNullOrWhiteSpace(dto.Activity) ||
               !string.IsNullOrWhiteSpace(dto.Name) ||
               !string.IsNullOrWhiteSpace(dto.Description) ||
               !string.IsNullOrWhiteSpace(dto.Outline);
    }
}