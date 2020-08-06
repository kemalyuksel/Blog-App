using Blog.Dto.DTOs.CategoryDtos;
using FluentValidation;

namespace Blog.Business.ValidationRules.FluentValidation
{
    public class CategoryUpdateValidator : AbstractValidator<CategoryUpdateDto>
    {
        public CategoryUpdateValidator()
        {
            RuleFor(x => x.Id).InclusiveBetween(0, int.MaxValue).
                WithMessage("This field is can not empty");
            RuleFor(x => x.Name).NotEmpty().WithMessage("This field is can not empty");
        }
    }
}
