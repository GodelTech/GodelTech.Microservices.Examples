using FluentValidation;
using Microservice.Api.Models;

namespace Microservice.Api.Validation
{
    public class UserNicknamesValidator : AbstractValidator<UserNicknames>
    {
        public UserNicknamesValidator()
        {
            RuleFor(x => x)
                .NotNull()
                .WithMessage("User name and nicknames must be provided");

            RuleFor(x => x.UserName)
                .NotEmpty()
                .OverridePropertyName("User name")
                .WithMessage("User name must be provided in request");

            RuleFor(x => x.Nicknames)
                .NotEmpty()
                .WithMessage("Nicknames must be provided in request");
        }
    }
}
