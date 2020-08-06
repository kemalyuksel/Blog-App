using Blog.Dto.DTOs.CategoryTopicDtos;
using FluentValidation;

namespace Blog.Business.ValidationRules.FluentValidation
{
    public class CategoryTopicValidator : AbstractValidator<CategoryTopicDto>
    {
        public CategoryTopicValidator()
        {
            RuleFor(x => x.CategoryId).InclusiveBetween(0,int.MaxValue).WithMessage
                ("This field is can not empty");
            RuleFor(x => x.TopicId).InclusiveBetween(0, int.MaxValue).WithMessage
               ("This field is can not empty");
        }
    }
}
