using Blog.Dto.DTOs.AppUserDtos;
using FluentValidation;

namespace Blog.Business.ValidationRules.FluentValidation
{
    public class AppUserLoginValidator : AbstractValidator<AppUserLoginDto>
    {

        public AppUserLoginValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("This field is can not empty");
            RuleFor(x => x.Password).NotEmpty().WithMessage("This field is can not empty");
        }

    }
}
