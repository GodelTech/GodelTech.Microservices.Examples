using FluentValidation;
using GodelTech.GraphQL.Web.Models;

namespace GodelTech.GraphQL.Web.Validators
{
    public class CreateOrUpdatePropertyNoteInputValidator : AbstractValidator<PropertyNoteInput>
    {
        public CreateOrUpdatePropertyNoteInputValidator()
        {
            CascadeMode = CascadeMode.Stop;

            RuleFor(_ => _.PropertyId).NotNull().NotEmpty();
            RuleFor(_ => _.Note).NotNull().NotEmpty();
        }
    }
}