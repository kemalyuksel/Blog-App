using Blog.Dto.DTOs.CategoryDtos;
using FluentValidation;

namespace Blog.Business.ValidationRules.FluentValidation
{
    public class CategoryAddValidator : AbstractValidator<CategoryAddDto>
    {

        public CategoryAddValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("This field is can not empty");
        }
    }
}
